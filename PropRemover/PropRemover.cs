﻿using System.Linq;
using UnityEngine;

namespace PropRemover
{

    public class PropRemover : MonoBehaviour
    {

        private static readonly string gameObjectName = "PropRemover";
        private static readonly string[] BillboardCategories = {
            "PropsBillboardsSmallBillboard",
            "PropsBillboardsMediumBillboard",
            "PropsBillboardsLargeBillboard",
            "PropsSpecialBillboardsRandomSmallBillboard",
            "PropsSpecialBillboardsRandomMediumBillboard",
            "PropsSpecialBillboardsRandomLargeBillboard",
        };

        public static void Initialize()
        {
            Dispose();
            var removerGo = new GameObject(gameObjectName);
            removerGo.AddComponent<PropRemover>();
        }

        public static void Dispose()
        {
            var removerGo = GameObject.Find(gameObjectName);
            if (removerGo == null)
            {
                return;
            }
            Destroy(removerGo);
        }

        public void Update()
        {
            RemoveProps();
            Dispose();
        }

        public static void RemoveProps()
        {
            var prefabs = Resources.FindObjectsOfTypeAll<BuildingInfo>();
            foreach (var buildingInfo in prefabs)
            {
                var fastList = new FastList<BuildingInfo.Prop>();
                if (buildingInfo == null)
                {
                    continue;
                }
                if (buildingInfo.m_props != null)
                {
                    var props = buildingInfo.m_props;
                    foreach (var prop in props.Where(prop => prop != null))
                    {
                        if (prop.m_finalProp != null)
                        {
                            if (
                                ((OptionsHolder.Options & ModOption.Smoke) == ModOption.None || !prop.m_finalProp.name.Contains("Smoke") && !prop.m_finalProp.name.Contains("smoke")) &&
                                ((OptionsHolder.Options & ModOption.Steam) == ModOption.None || !prop.m_finalProp.name.Contains("Steam") && !prop.m_finalProp.name.Contains("steam")) &&
                                ((OptionsHolder.Options & ModOption.ClownHeads) == ModOption.None || !prop.m_finalProp.name.Contains("Clown") && !prop.m_finalProp.name.Contains("clown")) &&
                                ((OptionsHolder.Options & ModOption.IceCreamCones) == ModOption.None || !prop.m_finalProp.name.Contains("Cream") && !prop.m_finalProp.name.Contains("cream")) &&
                                ((OptionsHolder.Options & ModOption.DoughnutSquirrels) == ModOption.None || !prop.m_finalProp.name.Contains("Squirrel") && !prop.m_finalProp.name.Contains("squirrel")) &&
                                ((OptionsHolder.Options & ModOption.Random3DBillboards) == ModOption.None || prop.m_finalProp.name != "Billboard_3D_variation") &&
                                ((OptionsHolder.Options & ModOption.FlatBillboards) == ModOption.None || !BillboardCategories.Contains(prop.m_finalProp.editorCategory))

                                )
                            {
                                fastList.Add(prop);
                            }
                        }
                        else
                        {
                            fastList.Add(prop);
                        }
                    }
                }
                buildingInfo.m_props = fastList.ToArray();
            }
        }
    }
}
