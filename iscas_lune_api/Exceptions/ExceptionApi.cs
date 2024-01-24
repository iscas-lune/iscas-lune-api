using iscaslune.Api.Errors;

namespace iscas_lune_api.Exceptions;

public class ExceptionApi : Exception
{
    public ExceptionApi(string error = ErrorResponseGeneric.Error) : base(error)
    { }
}
