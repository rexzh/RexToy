using System;
using System.Collections.Generic;

namespace RexToy.ORM.Configuration
{
    class DatabaseInfo : IDatabaseInfo
    {
        public DatabaseInfo(string cnnstr, string provider, string dialectId, string dialectProvider)
        {
            cnnstr.ThrowIfNullArgument(nameof(cnnstr));
            dialectId.ThrowIfNullArgument(nameof(dialectId));
            _connect_string = cnnstr;
            _dialectId = dialectId;
            _provider = provider;
            _dialectProvider = dialectProvider;
        }

        private string _connect_string;
        public string ConnectString
        {
            get { return _connect_string; }
        }

        private string _dialectId;
        public string DialectId
        {
            get { return _dialectId; }
        }

        private string _provider;
        public string Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        private string _dialectProvider;
        public string DialectProvider
        {
            get { return _dialectProvider; }
            set { _dialectProvider = value; }
        }
    }
}
