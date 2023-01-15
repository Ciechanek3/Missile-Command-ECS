using Marker;
using MissileLauncher;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Projectile
{
    [BurstCompile]
    [UpdateAfter(typeof(MissileLauncherSystem))]
    public partial class ProjectileSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnUpdate()
        {
            
            /*(new FireProjectileJob
            {
                //Destination = destination
            }.Run();*/
        }
        
    }
    
    [BurstCompile]
    public partial struct FireProjectileJob : IJobEntity
    {
        public float2 Destination;
        
        [BurstCompile]
        private void Execute(ProjectileAspect projectile)
        {
            projectile.SetDestination(Destination);
        }
    }
}


