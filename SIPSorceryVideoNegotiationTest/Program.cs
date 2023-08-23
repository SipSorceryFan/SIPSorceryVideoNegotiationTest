using System.Net;
using SIPSorcery.Media;
using SIPSorcery.SIP;
using SIPSorcery.SIP.App;
using SIPSorceryMedia.Abstractions;

namespace SIPSorceryVideoNegotiationTest
{
    class Program
    {
        static async Task Main()
        {
            var recipientSipTransport = new SIPTransport();

            recipientSipTransport.AddSIPChannel(new SIPUDPChannel(IPAddress.Any, 5556));
            recipientSipTransport.AddSIPChannel(new SIPUDPChannel(IPAddress.IPv6Any, 5556));
            recipientSipTransport.AddSIPChannel(new SIPTCPChannel(IPAddress.Any, 5556));

            var recipientUserAgent = new SIPUserAgent(recipientSipTransport, null, true);

            recipientUserAgent.OnIncomingCall += (agent, request) =>
            {
                var serverUserAgent = agent.AcceptCall(request);

                var singleFormatVideoSourceMediaEndPoints = new MediaEndPoints
                {
                    VideoSource = new SingleFormatVideoSource()
                };

                _ = agent.Answer(serverUserAgent, new VoIPMediaSession(singleFormatVideoSourceMediaEndPoints));
            };

            var callerSipTransport = new SIPTransport();

            callerSipTransport.AddSIPChannel(new SIPUDPChannel(IPAddress.Any, 5555));
            callerSipTransport.AddSIPChannel(new SIPUDPChannel(IPAddress.IPv6Any, 5555));
            callerSipTransport.AddSIPChannel(new SIPTCPChannel(IPAddress.Any, 5555));

            var callerUserAgent = new SIPUserAgent(callerSipTransport, null, true);

            var multiFormatVideoSourceMediaEndPoints = new MediaEndPoints
            {
                VideoSource = new MultiFormatVideoSource()
            };

            _ = await callerUserAgent.Call("sip:127.0.0.1:5556", null, null, new VoIPMediaSession(multiFormatVideoSourceMediaEndPoints));

            Console.ReadLine();
        }
    }
}