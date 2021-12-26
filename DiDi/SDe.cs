using System;

namespace DiDi
{
    public class SDe
        {
            public Type ST { get;}
    
            public Type IT { get;}
    
            public object Imple { get; internal set; }
    
            public SLt LT { get;}
    
            public SDe(Type sT, Type iT, SLt lT)
            {
                ST = sT;
                IT = iT;
                LT = lT;
            }
        }
}