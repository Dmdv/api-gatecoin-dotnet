using System;
using System.Collections.Generic;
using System.Web;
using ServiceStack.Common;
using ServiceStack;

using GatecoinServiceInterface.Response;
namespace GatecoinServiceInterface.Request{
[Route("/Account/Address", "GET", Summary = @"Get user address", Notes = @"")]
public class GetAddress : IReturn<AddressResponse>
{
}
}

