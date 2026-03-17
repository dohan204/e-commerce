namespace infrastructure.cache
{
    public class MemoryCacheOptions
    {
        public int AbsoluteExpiration {get; set;}
        public int SlidingExpiration {get; set;}
    }
}