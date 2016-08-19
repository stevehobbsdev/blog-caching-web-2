using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace CachingDemo.Web.Models.Data
{
	public interface IVehicleRepository
	{
		void ClearCache();
		IEnumerable<Vehicle> GetVehicles();
		void Insert(Vehicle vehicle);
		void Update(Vehicle vehicle);
		void SaveChanges();
	}

	public class VehicleRepository : IVehicleRepository
	{
		protected CachingDemoEntities DataContext { get; private set; }

		public ICacheProvider Cache { get; set; }

		public VehicleRepository()
			: this(new DefaultCacheProvider())
		{
		}

		public VehicleRepository(ICacheProvider cacheProvider)
		{
			this.DataContext = new CachingDemoEntities();			
			this.Cache = cacheProvider;
		}

		public IEnumerable<Vehicle> GetVehicles()
		{
			// First, check the cache
			var vehicleData = Cache.Get("vehicles") as IDictionary<Guid, Vehicle>;

			// If it's not in the cache, we need to read it from the repository
			if (vehicleData == null)
			{
				// Get the repository data
				vehicleData = DataContext.Vehicles.ToDictionary(v => v.Id);

				if (vehicleData.Any())
				{
					// Put this data into the cache for 30 minutes
					Cache.Set("vehicles", vehicleData, 30);
				}
			}

			return vehicleData.Values;
		}

		public void Update(Vehicle vehicle)
		{
			if (vehicle.EntityState == EntityState.Detached)
			{
				DataContext.AttachTo("Vehicles", vehicle);
			}
			DataContext.ObjectStateManager.ChangeObjectState(vehicle, EntityState.Modified);
		}

		public void Insert(Vehicle vehicle)
		{
			DataContext.AddToVehicles(vehicle);
		}

		public void ClearCache()
		{
			Cache.Invalidate("vehicles");
		}

		public void SaveChanges()
		{
			// Update or add new/existing entities from the changeset
			var changeset = DataContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified);
			
			DataContext.SaveChanges();
						
			var cacheData = Cache.Get("vehicles") as Dictionary<Guid, Vehicle>;

			if (cacheData != null)
			{
				foreach (var item in changeset)
				{
					var vehicle = item.Entity as Vehicle;
					cacheData[vehicle.Id] = vehicle;
				}
			}					
		}
	}
}