namespace EamaShop.Infrastructures
{
    public class RedisLockOptions
    {
        /// <summary>
        /// Default is localhost:6379
        /// </summary>
        public string Configuration { get; set; } = "localhost:6379";

        public string InstanceName { get; set; }
    }
}