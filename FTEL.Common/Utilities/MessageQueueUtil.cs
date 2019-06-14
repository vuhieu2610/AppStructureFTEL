using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common.Utilities
{
    public class MessageQueueUtil
    {
        public MessageQueue MsgQueue { get; set; }

        public bool CreateMessageQueue(string messageQueuePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(messageQueuePath))
                {
                    if (MessageQueue.Exists(messageQueuePath))
                    {
                        MsgQueue = new MessageQueue(messageQueuePath);
                        return true;
                    }
                    else
                    {
                        MsgQueue = MessageQueue.Create(messageQueuePath);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.Message, ex);
                return false;
            }
            return false;
        }


        public bool AddDataToMessageQueue<T>(T obj, bool recoverable) where T : new()
        {
            if (null != MsgQueue)
            {
                try
                {
                    Message msg = new Message();
                    string json = JsonConvert.SerializeObject(obj);
                    msg.Formatter = new BinaryMessageFormatter();
                    msg.Body = json;
                    msg.Label = string.Empty;
                    msg.Priority = MessagePriority.High;
                    MsgQueue.Send(msg);
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.Error(ex.Message, ex);
                }
            }
            return false;
        }

        public bool AddListDataToMessageQueue<T>(List<T> objs, bool recoverable) where T : new()
        {
            try
            {
                if (null != MsgQueue)
                {
                    foreach (T obj in objs)
                    {
                        Message msg = new Message();
                        msg.Recoverable = recoverable;
                        string json = JsonConvert.SerializeObject(obj);
                        msg.Formatter = new BinaryMessageFormatter();
                        msg.Body = json;
                        msg.Label = string.Empty;
                        msg.Priority = MessagePriority.High;
                        MsgQueue.Send(msg);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.Message, ex);
                return false;
            }
            return false;
        }

        public T GetDataFromMessageQueue<T>() where T : new()
        {
            if (null != MsgQueue)
            {
                try
                {
                    Message msg = MsgQueue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    string msgString = msg.Body.ToString();
                    if (!string.IsNullOrEmpty(msgString))
                    {
                        T obj = JsonConvert.DeserializeObject<T>(msgString);
                        return obj;
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.Error(ex.Message, ex);
                }
            }
            return default(T);
        }


        public bool Delete()
        {
            if (null != MsgQueue)
            {
                try
                {
                    MessageQueue.Delete(MsgQueue.Path);
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.Error(ex.Message, ex);
                    return false;
                }
            }
            return false;
        }
    }
}
