using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
  public  class BusinessRules
    {
        // this syntax basicly means you can give as many params as you want of type IResult
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
                 
            }
            return null;
        }
    }
}
