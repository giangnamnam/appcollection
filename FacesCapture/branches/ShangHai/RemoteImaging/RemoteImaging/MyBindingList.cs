using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public class MyBindingList<T> : BindingList<T>
    {
        public MyBindingList(IList<T> list) :base(list)
        {
            
        }

        protected override void RemoveItem(int index)
        {
            if (BeforeRemove != null)
                BeforeRemove(this,
                      new ListChangedEventArgs(ListChangedType.ItemDeleted, index));

            base.RemoveItem(index);
        }

        public event EventHandler<ListChangedEventArgs> BeforeRemove;
    }
}
