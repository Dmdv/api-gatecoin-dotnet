using System;
using System.Collections.Generic;
using System.Web;
using ServiceStack.Common;
using ServiceStack;

using GatecoinServiceInterface.Response;
namespace GatecoinServiceInterface.Request{
[Route("/Reference/Currencies", "GET", Summary = @"Get the currency list.", Notes = @"")]
public class GetCurrencies : IReturn<GetCurrenciesResponse>
{
}
}

