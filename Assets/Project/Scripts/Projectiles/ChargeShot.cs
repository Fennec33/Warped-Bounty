using WarpedBounty.Pooling;

namespace WarpedBounty.Projectiles
{
    public class ChargeShot : Projectile
    {
        protected override void ReturnToPool()
        {
            ChargeShotPool.Instance.ReturnToPool(this);
        }
    }
}