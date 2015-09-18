using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ScratchConsole
{
    public class ScratchController : ApiController
    {
        private static string s_command;
        private static int s_commandCounter;
        private static object s_commandLock = new object();


        internal static void NewCommand(string command)
        {
            lock (s_commandLock)
            {
                s_commandCounter++;
                s_command = command;
            }
        }

        [Route("poll")]
        [HttpGet]
        public HttpResponseMessage Poll()
        {
            var b = new StringBuilder();

            lock (s_commandLock)
            {
                b.Append("Command ");
                b.Append(s_command);
                b.Append(Environment.NewLine);
                b.Append("CommandCounter ");
                b.Append(s_commandCounter);
                b.Append(Environment.NewLine);
            }

            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent(b.ToString(), Encoding.UTF8, "text/plain");
            return resp;
        }

        [Route("log/{message}")]
        [HttpGet]
        public HttpResponseMessage Log(string message)
        {
            Console.Write(message);
            return this.OkResponse;
        }

        [Route("logLine/{message}")]
        [HttpGet]
        public HttpResponseMessage LogLine(string message)
        {
            Console.WriteLine(message);
            return this.OkResponse;
        }

        [Route("clearScreen")]
        [HttpGet]
        public HttpResponseMessage ClearScreen()
        {
            Console.Clear();
            return this.OkResponse;
        }

        [Route("setForegroundColor/{color}")]
        [HttpGet]
        public HttpResponseMessage SetForegroundColor(string color)
        {
            ConsoleColor c;
            if (!Enum.TryParse<ConsoleColor>(color, out c))
            {
                c = ConsoleColor.Gray;
            }
            Console.ForegroundColor = c;
            return this.OkResponse;
        }

        [Route("setBackgroundColor/{color}")]
        [HttpGet]
        public HttpResponseMessage SetBackgroundColor(string color)
        {
            ConsoleColor c;
            if (!Enum.TryParse<ConsoleColor>(color, out c))
            {
                c = ConsoleColor.Black;
            }
            Console.BackgroundColor = c;

            return this.OkResponse;
        }

        private HttpResponseMessage OkResponse
        {
            get
            {
                var resp = new HttpResponseMessage(HttpStatusCode.OK);
                resp.Content = new StringContent(string.Empty, Encoding.UTF8, "text/plain");
                return resp;
            }
        }
    }
}
