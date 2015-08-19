using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM.QueryModel
{
    public class Query : IQuery
    {
        public Query(View view, Criteria criteria)
        {
            _view = view;
            //Note:criteria can be null, view.OrderBy(..) or view.AsQuery()
            _criteria = criteria;
        }

        private View _view;
        public View View
        {
            get { return _view; }
        }

        private Criteria _criteria;
        public Criteria Criteria
        {
            get { return _criteria; }
            internal set { _criteria = value; }
        }

        private OrderCollection _orders;
        public OrderCollection Order
        {
            get { return _orders; }
            internal set { _orders = value; }
        }
    }
}
