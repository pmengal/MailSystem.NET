using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveUp.Net.Samples.Utils
{
    public class ItemTag
    {
        #region Fields

        private string _text;
        private object _tag;

        #endregion

        #region Constructor

        public ItemTag()
        {
        }

        public ItemTag(string text, object tag)
        {
            _text = text;
            _tag = tag;
        }

        #endregion

        #region Properties

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return _text;
        }

        #endregion
    }
}
