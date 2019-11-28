using Xunit;

namespace ShortUrl.Tests
{
    public class ShortUrlTests
    {
        [Fact]
        public void TestEncodeSteps()
        {
            var instance = new UrlEncoder();
            var encode = instance.Encode(2);
            Assert.Equal(4194304, encode);
            var enbase = instance.Enbase(encode);
            Assert.Equal("25t52", enbase);
        }

        [Fact]
        public void TestDecodeSteps()
        {
            var instance = new UrlEncoder();
            var debase = instance.Debase("def");
            Assert.Equal(20923, debase);
            var decode = instance.Decode(debase);
            Assert.Equal(14518784, decode);
        }

        [Fact]
        public void TestCustomAlphabet()
        {
            var instance = new UrlEncoder("ab");
            var encode = instance.Encode(12);
            Assert.Equal(3145728, encode);
            var url = instance.EncodeUrl(12);
            Assert.Equal("bbaaaaaaaaaaaaaaaaaaaa", url);
            var key = instance.DecodeUrl("bbaaaaaaaaaaaaaaaaaaaa");
            Assert.Equal(12, key);
        }

        [Theory]
        [InlineData(0, "mmmmm")]
        [InlineData(1, "867nv")]
        [InlineData(2, "25t52")]
        [InlineData(108130, "2quaa")]
        [InlineData(507842, "2xzau")]
        [InlineData(603936, "mbxtx")]
        public void TestCalculatedValues(int id, string key)
        {
            var instance = new UrlEncoder();
            Assert.Equal(key, instance.EncodeUrl(id));
            Assert.Equal(id, instance.DecodeUrl(key));
        }
    }
}
