using ChatApi.BusinessLayer.Concrete;
using ChatApi.DataAccessLayer.Concrete;
using ChatApi.DataAccessLayer.EntityFramework;
using ChatApi.EntityLayer;
using ChatApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    public class MessageController : Controller
    {
        MessageManager _messageManager = new MessageManager(new EfMessageRepository());


        [HttpGet("[action]{receiverId},{senderId}")]
        public IActionResult UserFriendList(int receiverId, int senderId)
        {
            var values = _messageManager.TGetBySenderIdAndReceiverId(receiverId,senderId);
            if (values == null)
            {
                return BadRequest("Hiç mesaj bulunmamaktadır.");
            }
            else
            {
                var messageContex = values.Select(message => new MessageContex
                {
                    MessageId = message.MessageId,
                    SenderId = message.SenderId,
                    MessageContext= message.MessageContext,
                    MessageReading= message.MessageReading,
                    MessageTime= message.MessageTime,
                    MessageType= message.MessageType,
                });
    
                return Ok(messageContex);
            }
            
        }


        [HttpPost("[action]")]
        public IActionResult AddMessage([FromBody] Message message)
        {

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();
                return BadRequest(new Result<Message>(errorMessages));
            }
            else
            {
                    _messageManager.TAdd(message);

                var messageViewModel = new Message
                {
                    MessageId = message.MessageId,
                    SenderId = message.SenderId,
                    ReceiverId = message.ReceiverId,
                    GroupId = message.GroupId,
                    MessageType = message.MessageType,
                    MessageContext = message.MessageContext,
                    SenderMessageStatus = message.SenderMessageStatus,
                    ReceiverMessageStatus = message.ReceiverMessageStatus,
                    MessageReading = message.MessageReading,
                    MessageTime = message.MessageTime,
                    

                   
                };
                return Ok(new Result<Message>(messageViewModel));
            }
            

        }


        [HttpPut("[action]{messageId},{userId}")]
        public  IActionResult DeleteMessage(int messageId,int userId)
        {
            var values=_messageManager.TGetById(messageId);
            if (values != null)
            {
                if(values.SenderId==userId)
                {
                    values.SenderMessageStatus = true;
                }
                else
                {
                    values.ReceiverMessageStatus = true;
                }
                _messageManager.TUpdate(values);
                return Ok(true);
            }
            else
            {
                return BadRequest(false);
            }

        }

        [HttpPut("[action]{messageId},{messageContext}")]
        public IActionResult EditMessage(int messageId, string messageContext)
        {
            var values = _messageManager.TGetById(messageId);
            if (values != null)
            {
                values.MessageContext = messageContext;
                _messageManager.TUpdate(values);
                return Ok(true);
            }
            else
            {
                return BadRequest(false);
            }
        }

        [HttpPut("[action]{messageId}")]
        public IActionResult ReadingMessage(int messageId)
        {
            var values = _messageManager.TGetById(messageId);
            if (values != null)
            {
                values.MessageReading = true;
                _messageManager.TUpdate(values);
                return Ok(true);
            }
            else
            {
                return BadRequest(false);
            }
        }

        [HttpGet("[action]{receiverId}")]
        public IActionResult AllMessageShow(int receiverId)
        {
            var values = _messageManager.TGetUserFriendMessageCount(receiverId);
            if (values != null)
            {
                return Ok(values);
            }
            else
            {
                return BadRequest(false);
            }
        }
    }
}
