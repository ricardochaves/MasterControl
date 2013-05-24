using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;


namespace ServidorWeb.PgBot
{
    public class PKGItens
    {

        private StaticDados sd = new StaticDados();

        public ItemBlizzard RetornaItemBlizzard(string codigo)
        {
            try
            {
                AcessoWEB aweb = new AcessoWEB();
                string jItem;

                jItem = aweb.RecuperaJSON(sd.host + sd.item + codigo);

                return ConverteJsontoItem(jItem);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na rotina RetornaItemBlizzard. {0} codigo: {1} {0}", Environment.NewLine, codigo), ex);
            }
        }

        private ItemBlizzard ConverteJsontoItem(string jItem)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var Obj = (Dictionary<string, object>)js.DeserializeObject(jItem);
                ItemBlizzard NovoItem = new ItemBlizzard();

                NovoItem.armor = Convert.ToInt32(Obj["armor"].ToString());
                NovoItem.baseArmor = Convert.ToInt32(Obj["baseArmor"].ToString());
                NovoItem.buyPrice = Convert.ToInt32(Obj["buyPrice"].ToString());
                NovoItem.containerSlots = Convert.ToInt32(Obj["containerSlots"].ToString());
                NovoItem.description = Obj["description"].ToString();
                NovoItem.disenchantingSkillRank = Convert.ToInt32(Obj["disenchantingSkillRank"].ToString());
                NovoItem.displayInfoId = Convert.ToInt32(Obj["displayInfoId"].ToString());
                NovoItem.equippable = Convert.ToBoolean(Obj["equippable"].ToString());
                NovoItem.hasSockets = Convert.ToBoolean(Obj["hasSockets"].ToString());
                NovoItem.heroicTooltip = Convert.ToBoolean(Obj["heroicTooltip"].ToString());
                NovoItem.icon = Obj["icon"].ToString();
                NovoItem.id = Convert.ToInt32(Obj["id"].ToString());
                NovoItem.inventoryType = Convert.ToInt32(Obj["inventoryType"].ToString());
                NovoItem.isAuctionable = Convert.ToBoolean(Obj["isAuctionable"].ToString());
                NovoItem.itemBind = Convert.ToInt32(Obj["itemBind"].ToString());
                NovoItem.itemClass = Convert.ToInt32(Obj["itemClass"].ToString());
                NovoItem.itemLevel = Convert.ToInt32(Obj["itemLevel"].ToString());
                NovoItem.itemSubClass = Convert.ToInt32(Obj["itemSubClass"].ToString());
                NovoItem.maxCount = Convert.ToInt32(Obj["maxCount"].ToString());
                NovoItem.maxDurability = Convert.ToInt32(Obj["maxDurability"].ToString());
                NovoItem.minFactionId = Convert.ToInt32(Obj["minFactionId"].ToString());
                NovoItem.minReputation = Convert.ToInt32(Obj["minReputation"].ToString());
                NovoItem.name = Obj["name"].ToString();
                NovoItem.nameDescription = Obj["nameDescription"].ToString();
                NovoItem.nameDescriptionColor = Obj["nameDescriptionColor"].ToString(); ;
                NovoItem.quality = Convert.ToInt32(Obj["quality"].ToString());
                NovoItem.requiredLevel = Convert.ToInt32(Obj["requiredLevel"].ToString());
                NovoItem.requiredSkill = Convert.ToInt32(Obj["requiredSkill"].ToString());
                NovoItem.requiredSkillRank = Convert.ToInt32(Obj["requiredSkillRank"].ToString());
                NovoItem.sellPrice = Convert.ToInt32(Obj["sellPrice"].ToString());
                NovoItem.stackable = Convert.ToInt32(Obj["stackable"].ToString());
                NovoItem.upgradable = Convert.ToBoolean(Obj["upgradable"].ToString());

                //NovoItem.bonusStats;
                //NovoItem.itemSource;
                //NovoItem.itemSpells;

                return NovoItem;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na rotina ConverteJsontoItem. {0} jItem: {1} {0}", Environment.NewLine, jItem), ex);
            }
        }




    }
}