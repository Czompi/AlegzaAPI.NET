using AlegzaCRM.AlegzaAPI.Model;
using System;
using System.Collections.Generic;

namespace AlegzaCRM.AlegzaAPI.Exceptions;

[Serializable]
public class APIException : Exception
{
    public IDictionary<string, ICollection<string>> ErrorList { get; }

    public APIException(AlegzaModel result) : base(result.ErrorMessage)
    {
        ErrorList = result.Errors;
    }
}
