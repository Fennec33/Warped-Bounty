using WarpedBounty.Pooling;

namespace WarpedBounty.Projectiles
{
    public class Shot : Projectile
    {
        protected override void ReturnToPool()
        {
            ShotPool.Instance.ReturnToPool(this);
        }
    }
}
