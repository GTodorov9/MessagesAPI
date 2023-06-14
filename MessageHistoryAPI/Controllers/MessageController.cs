using MessageHistoryAPI.BindingModels;
using MessageHistoryAPI.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MessageHistoryAPI.Controllers
{
    [ApiController]

    public class MessageController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MessageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("api/Message/SaveMessages")]
        public ActionResult SaveMessages([FromBody] SaveMessagesBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var dbMessages = bindingModel.Messages.Select(e => new Message
                {
                    Content = e.Content,
                    FromUser_Id = e.FromUser_Id,
                    ToUser_Id = e.ToUser_Id,
                    TimeStamp = e.TimeStamp
                }).ToList();

                using (var dbContext = new MessageDBContext(_configuration))
                {
                    dbContext.Messages.AddRange(dbMessages);
                    dbContext.SaveChanges();
                }

            }
            else
            {
                return StatusCode(500);
            }

            return Ok("Messaged added to DB.");
        }
        [HttpGet]
        [Route("api/Message/GetMessagesReport")]
        public ActionResult GetMessagesReport(DateTime fromDate, DateTime toDate)
        {
            using (var dbContext = new MessageDBContext(_configuration))
            {
                var stats = dbContext.Messages
            .GroupBy(m => 1)
            .Select(g => new
            {
                TotalMessages = g.Count(),
                TotalUsers = g.Select(m => m.FromUser_Id).Distinct().Count(),
                AverageContentLength = g.Average(m => m.Content.Length),
                MaxContentLength = g.Max(m => m.Content.Length),
                ShortestMessage = g.OrderBy(m => m.Content.Length).FirstOrDefault().Content,
                LongestMessage = g.OrderByDescending(m => m.Content.Length).FirstOrDefault().Content,
                MostActiveUser = g.GroupBy(m => m.FromUser_Id)
                    .OrderByDescending(gu => gu.Count())
                    .Select(gu => new
                    {
                        Username = gu.First().FromUser_Id,
                        MessageCount = gu.Count()
                    })
                    .FirstOrDefault()
            })
            .FirstOrDefault();
                return Ok(stats);
            }


        }
        [HttpGet]
        [Route("api/Message/GetMessagesForUsers")]
        public ActionResult GetMessagesForUsers(string fromUserId, string toUserId)
        {
            using (var dbContext = new MessageDBContext(_configuration))
            {
                var msgs = dbContext.Messages.Where(e =>
                    (e.ToUser_Id == toUserId && e.FromUser_Id == fromUserId)
                    ||
                    (e.FromUser_Id == toUserId && e.ToUser_Id == fromUserId)
                    ).Select(m => new
                    {
                        FromUser_Id = m.FromUser_Id,
                        ToUser_Id = m.ToUser_Id,
                        PublishedOn = m.TimeStamp,
                        Content = m.Content
                    }).ToList();

                return Ok(msgs);
            }


        }

    }
}