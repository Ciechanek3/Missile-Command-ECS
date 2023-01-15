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
            var mp = SystemAPI.GetSingletonEntity<MarkerProperties>();
            var markerAspect = SystemAPI.GetAspectRW<MarkerAspect>(mp);
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            new FireProjectileJob
            {
                Destination = markerAspect.Position,
                DeltaTime = deltaTime
            }.Run();
        }
        
    }
    
    [BurstCompile]
    public partial struct FireProjectileJob : IJobEntity
    {
        public float2 Destination;
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(ProjectileAspect projectile)
        {
            if (projectile.CheckIfOnDestination()) return;
                projectile.SetDestination(Destination);
            projectile.Fire(DeltaTime);
        }
    }
}


