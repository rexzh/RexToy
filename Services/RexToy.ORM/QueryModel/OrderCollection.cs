using System;
using System.Collections;
using System.Collections.Generic;

namespace RexToy.ORM.QueryModel
{
    public class OrderCollection : IEnumerable<Order>
    {
        private List<Order> _orders;
        internal OrderCollection()
        {
            _orders = new List<Order>();
        }

        internal void AppendOrder(Order order)
        {
            order.ThrowIfNullArgument(nameof(order));
            
            _orders.Add(order);
        }

        public IEnumerator<Order> GetEnumerator()
        {
            return _orders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _orders.GetEnumerator();
        }
    }
}
