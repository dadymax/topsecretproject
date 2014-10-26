using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realisations.Vehicles
{
    public class Vehicle : Common.HaveHeapthPoints, Common.IHaveName, Common.IHaveMass
    {
        public Vehicle()
        {
        }

        void Vehicle_massTotalChanged()
        {
            uint sum = Mass;
            sum += weapons.Sum(w => (long)w.Value.Mass);
            sum += engines.Sum(w => (long)w.Value.Mass);
            sum += equipment.Sum(w => (long)w.Value.Mass);
            massTotalCache = sum;
        }

        //some attributes
        //
        public Common.Coordinates coordinates;

        public string Name { get; set; }

        public uint Mass { get; set; }

        protected uint massTotalCache = 0;

        public uint MassTotal { get { return massTotalCache; } }

        //weapon
        //slotsystem: vehicle have 0 to n slots rectangle slots. Any slot can be equippen by 1 weapon that can fit into.
        public ItemDictionary<Weapons.VehicleWeaponSlot, Weapons.VehicleWeaponItem> weapons;

        //engine
        //slotsystem
        public ItemDictionary<Engines.VehicleEngineSlot, Engines.VehicleEngineItem> engines;

        //equipment
        //space system: vehicle have free space as rectangle. Free space divided to square 1x1.
        public ItemDictionary<Equipment.VehicleEquipmentGridspace, Equipment.VehicleEquipmentItem> equipment;



        public class ItemDictionary<T, U>
            where T : class
            where U : class
        {
            Vehicle vehicle;
            protected Dictionary<T, U> innerDict;
            public ItemDictionary(Vehicle vehicle)
            {
                this.vehicle = vehicle;
                innerDict = new Dictionary<T, U>();
            }

            public T Add(T key, U value)
            {
                innerDict.Add(key, value);
                vehicle.Vehicle_massTotalChanged();
                return key;
            }

            public T Remove(T key)
            {
                innerDict.Remove(key);
                vehicle.Vehicle_massTotalChanged();
                return key;
            }

            public U this[T key]
            {
                get { return innerDict[key]; }
                set
                {
                    innerDict[key] = value;
                    vehicle.Vehicle_massTotalChanged();
                }
            }

            public U this[string tag]
            {
                get
                {
                    KeyValuePair<T, U> pair = innerDict.FirstOrDefault(t => (t.Key as Common.IHaveTag).Tag == tag);

                    if (pair.Equals(default(KeyValuePair<T, U>)))
                        return null;

                    return pair.Value;
                }
                set
                {
                    KeyValuePair<T, U> pair = innerDict.FirstOrDefault(t => (t.Key as Common.IHaveTag).Tag == tag);

                    if (pair.Equals(default(KeyValuePair<T, U>)))
                        return;

                    innerDict[pair.Key] = value;
                    vehicle.Vehicle_massTotalChanged();
                }
            }

            public uint Sum(Func<KeyValuePair<T, U>, long> condition)
            {
                return (uint)innerDict.Sum(condition);
            }
        }
    }
}

