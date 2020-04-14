﻿using SimpleJSON;
using System.Linq;
using UnityEngine;

namespace ToySerialController.Utils
{
    public static class Extensions
    {
        public static Rigidbody GetRigidBodyByName(this Atom atom, string name)
            => atom.rigidbodies?.FirstOrDefault(b => b.name == name);

        public static T GetComponentByName<T>(this Component component, string name) where T : Component
            => component.GetComponentsInChildren<T>()?.FirstOrDefault(c => c.name == name);

        public static void Store(this JSONNode config, JSONStorableParam storable)
        {
            const string dummyNodeName = "Dummy";

            var dummyNode = new JSONClass();
            var oldName = storable.name;

            storable.name = dummyNodeName;
            storable.StoreJSON(dummyNode, forceStore: true);
            storable.name = oldName;

            var nodeNames = storable.name.Split(':');
            var node = config;
            foreach(var name in nodeNames.Take(nodeNames.Length - 1))
                node = node[name];

            node[nodeNames.Last()] = dummyNode[dummyNodeName];
        }

        public static void Restore(this JSONNode config, JSONStorableParam storable)
        {
            const string dummyNodeName = "Dummy";

            var dummyNode = new JSONClass();
            var oldName = storable.name;

            var nodeNames = storable.name.Split(':');
            var node = config;
            foreach (var name in nodeNames.Take(nodeNames.Length - 1))
                node = node[name];

            dummyNode[dummyNodeName] = node[nodeNames.Last()];

            storable.name = dummyNodeName;
            storable.RestoreFromJSON(dummyNode);
            storable.name = oldName;
        }
    }
}