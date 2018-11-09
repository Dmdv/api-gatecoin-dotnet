using System;
using System.Collections.Generic;
using System.Web;
using ServiceStack.Common;
using ServiceStack;

using GatecoinServiceInterface.Response;
namespace GatecoinServiceInterface.Request{
[Route("/Trade/StopOrders", "DELETE", Summary = @"Cancels all existing stop orders", Notes = @"")]
public class CancelStopOrders : IReturn<CommonResponse>
{
}
}

