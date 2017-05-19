using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Android.Core
{ 
        public class ResizeOperation
        {
            public ResizeOperation(ResizeType type, int width, int height)
            {
                method = type;
                this.width = width;
            this.height = height;
            }

            public int width { get; }
            public int height { get; }
            public ResizeType method { get; }
        }

        public enum ResizeType
        {
            Fit,
            Scale,
            Cover
        }

}
