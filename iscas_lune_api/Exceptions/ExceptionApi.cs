namespace iscas_lune_api.Exceptions;

public class ExceptionApi : Exception
{
    const string Error = "Ocorreu um erro interno, tente novamente mais tarde!";

    public ExceptionApi(string error = Error) : base(error)
    { }
}
