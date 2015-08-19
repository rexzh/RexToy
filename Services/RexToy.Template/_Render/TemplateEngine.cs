using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using RexToy.Collections;
using RexToy.ExpressionLanguage;

namespace RexToy.Template
{
    using RexToy.Template.AST;

    public class TemplateEngine : ITemplateEngine
    {
        /// <summary>
        /// Build up a template engine.
        /// </summary>
        /// <param name="path">All template files is supposed in a specified directory(include sub directory).
        ///                    If it is null, will use current app directory.
        /// </param>
        /// <param name="ctx">Template context, also work as EL context when eval expresion in the template.</param>
        /// <param name="cache">Template AST cache.</param>
        /// <param name="cfg">the EL engine (which used in eval expression in the template) create config.</param>
        /// <returns>Instance of template engine.</returns>
        public static TemplateEngine CreateInstance(string path = null, ITemplateContext ctx = null, ICache<string, TemplateAST> cache = null, ExpressionLanguageEngineConfig cfg = null)
        {
            return new TemplateEngine(path, ctx, cache, cfg);
        }

        private ICache<string, TemplateAST> _cache;

        private string _path;
        public string Path
        {
            get { return _path; }
        }

        private ITemplateContext _ctx;
        public ITemplateContext Context
        {
            get { return _ctx; }
        }

        private ExpressionLanguageEngineConfig _cfg;
        private TemplateEngine(string path, ITemplateContext ctx, ICache<string, TemplateAST> cache, ExpressionLanguageEngineConfig cfg)
        {
            _ctx = ctx ?? new TemplateContext();
            _cache = cache ?? NoCache.GetInstance<string, TemplateAST>();
            _cfg = cfg ?? ExpressionLanguageEngineConfig.Default;

            if (string.IsNullOrEmpty(path))
                _path = Runtime.StartupDirectory;
            else
                _path = System.IO.Path.GetFullPath(path);
        }

        internal TemplateAST Parse(string fileName)
        {
            TemplateParser tp = new TemplateParser(this);
            return tp.Parse(fileName);
        }

        /// <summary>
        /// Render a template
        /// </summary>
        /// <param name="fileName">Only file name here, no directory info, otherwise will cause Exception</param>
        /// <returns></returns>
        public string Render(string fileName)
        {
            fileName.ThrowIfNullArgument(nameof(fileName));
            if (fileName.IndexOf(System.IO.Path.DirectorySeparatorChar) != -1 || fileName.IndexOf(System.IO.Path.AltDirectorySeparatorChar) != -1)
                ExceptionHelper.ThrowOnlyFileNameAllowed(fileName);

            try
            {
                TemplateAST ast = _cache.Get(fileName);
                if (ast == null)
                {
                    ast = this.Parse(fileName);
                    _cache.Put(fileName, ast);
                }
                return this.Render(ast);
            }
            catch (ELParseException)
            {
                throw;
            }
            catch (EvalException)
            {
                throw;
            }
            catch (TemplateParseException)
            {
                throw;
            }
            catch (TemplateRenderException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowWrapped(ex);
                return string.Empty;
            }
        }

        internal string Render(TemplateAST ast)
        {
            TemplateVisitor v = new TemplateVisitor(_ctx, _cfg);
            ast.Root.Accept(v);
            return v.Result;
        }

        public string RenderRaw(string textTemplate)
        {
            textTemplate.ThrowIfNullArgument(nameof(textTemplate));

            try
            {
                LexicalParser lp = new LexicalParser();
                lp.SetParseContent(textTemplate);
                SemanticParser sp = new SemanticParser();
                sp.SetParseContent(lp.Parse());
                TemplateAST ast = sp.Parse();

                return this.Render(ast);
            }
            catch (ELParseException)
            {
                throw;
            }
            catch (EvalException)
            {
                throw;
            }
            catch (TemplateParseException)
            {
                throw;
            }
            catch (TemplateRenderException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowWrapped(ex);
                return string.Empty;
            }
        }
    }
}
