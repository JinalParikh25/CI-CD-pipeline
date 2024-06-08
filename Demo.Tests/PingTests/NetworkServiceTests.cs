using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.Ping;

//using NetworkUtility.Ping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Tests.PingTests
{
    public class NetworkServiceTests
    {
        private readonly NetworkService _networkService;
       // private readonly IDNS _dNs;
        public NetworkServiceTests()
        {
           // _dNs = A.Fake<IDNS>();

             _networkService = new NetworkService();
        }


        /// <summary>
        /// Test String
        /// </summary>
        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            //Arrange
          //  A.CallTo(() => _dNs.SendDNS()).Returns(true);
            //var pingService = new NetworkService();

            //Act
            var result = _networkService.SendPing();

            //Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Suceess: Ping Sent");
            result.Should().Contain("Suceess", Exactly.Once());

        }


        /// <summary>
        /// Test Int
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="expectedValue"></param>
        [Theory]
        [InlineData(1,1,2)]
        [InlineData(2,2,4)]
        public void NetworkService_SendPong_ReturnInt(int a, int b, int expectedValue)
        {
            //var pingService = new NetworkService();

            var result = _networkService.PingTimeOut(a, b);

            result.Should().Be(expectedValue);
            result.Should().BeGreaterThanOrEqualTo(2);
            result.Should().NotBeInRange(-10000, 0);
        }


        /// <summary>
        /// Test DateTime
        /// </summary>
        [Fact]
        public void NetworkService_LastPingDate_ReturnDateTime()
        {
            //Arrannge

            //Act
            var result = _networkService.LastPingDate();

            //Assert
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2040));
        }

        /// <summary>
        /// Testing Object
        /// </summary>
        [Fact]
        public void NetworkService_GetPingOptions_ReturnObject()
        {
            //Arrange
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };

            //Act
            var objPing = _networkService.GetPingOptions();

            //Assert
            objPing.Should().BeEquivalentTo(expected);
            objPing.Ttl.Should().Be(1);
        }


        /// <summary>
        /// Test IEnumerable
        /// </summary>

        [Fact]
        public void NetworkService_MostRecentPings_ReturnObject()
        {
            //Arrange
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };

            //Act
            var result = _networkService.MostRecentPings();

            //Assert
           // result.Should().BeOfType<IEnumerable<PingOptions>>();
            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);
        }
    }
}
