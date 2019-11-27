using Xunit;

namespace ShortUrl.Tests
{
    public class ShortUrlTests
    {
        [Fact]
        public void CanEncodeAsExpected()
        {
            var instance = new UrlEncoder();
            var encode = instance.Encode(2);
            Assert.Equal(4194304, encode);
            var enbase = instance.Enbase(encode);
            Assert.Equal("25t52", enbase);
        }

        [Fact]
        public void CanDecodeAsExpected()
        {
            var instance = new UrlEncoder();
            var debase = instance.Debase("def");
            Assert.Equal(20923, debase);
            var decode = instance.Decode(debase);
            Assert.Equal(14518784, decode);
        }

        [Fact]
        public void CanSetCustomAlphabet()
        {
            var instance = new UrlEncoder("ab");
            var encode = instance.Encode(12);
            Assert.Equal(3145728, encode);
            var url = instance.EncodeUrl(12);
            Assert.Equal("bbaaaaaaaaaaaaaaaaaaaa", url);
            var key = instance.DecodeUrl("bbaaaaaaaaaaaaaaaaaaaa");
            Assert.Equal(12, key);
        }
    }
}
