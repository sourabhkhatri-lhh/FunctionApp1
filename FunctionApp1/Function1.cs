// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            var result = ExecuteTask();
            log.LogInformation(eventGridEvent.Data.ToString());
        }

        static async Task ExecuteTask()
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("testsendgridmail@example.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("testsendgridmail1@mailinator.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

    }
}
