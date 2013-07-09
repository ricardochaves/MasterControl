using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ServidorWeb.BD;

namespace ServidorWeb.PgBot
{
    public class PKGItens
    {

        private StaticDados sd = new StaticDados();

        private Item RetornaItemBlizzard(string codigo)
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

        private Item ConverteJsontoItem(string jItem)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var Obj = (Dictionary<string, object>)js.DeserializeObject(jItem);
                Item NovoItem = new Item();

                //NovoItem.armor = Convert.ToInt32(Obj["armor"].ToString());
                //NovoItem.baseArmor = Convert.ToInt32(Obj["baseArmor"].ToString());
                NovoItem.buyPrice = Convert.ToInt32(Obj["buyPrice"].ToString());
                NovoItem.containerSlots = Convert.ToInt32(Obj["containerSlots"].ToString());
                NovoItem.Desc = Obj["name"].ToString();
                //NovoItem.disenchantingSkillRank = Convert.ToInt32(Obj["disenchantingSkillRank"].ToString());
                //NovoItem.displayInfoId = Convert.ToInt32(Obj["displayInfoId"].ToString());
                //NovoItem.equippable = Obj["equippable"].ToString();
                //NovoItem.hasSockets = Convert.ToBoolean(Obj["hasSockets"].ToString());
                //NovoItem.heroicTooltip = Convert.ToBoolean(Obj["heroicTooltip"].ToString());
                NovoItem.icon = Obj["icon"].ToString();
                NovoItem.id = Convert.ToInt32(Obj["id"].ToString());
                NovoItem.inventoryType = Convert.ToInt32(Obj["inventoryType"].ToString());
                //NovoItem.isAuctionable = Convert.ToBoolean(Obj["isAuctionable"].ToString());
                NovoItem.itemBind = Obj["itemBind"].ToString();
                NovoItem.itemClass = Convert.ToInt32(Obj["itemClass"].ToString());
                NovoItem.itemLevel = Convert.ToInt32(Obj["itemLevel"].ToString());
                NovoItem.itemSubClass = Convert.ToInt32(Obj["itemSubClass"].ToString());
                NovoItem.maxCount = Convert.ToInt32(Obj["maxCount"].ToString());
                NovoItem.maxDurability = Convert.ToInt32(Obj["maxDurability"].ToString());
                NovoItem.minFactionId = Convert.ToInt32(Obj["minFactionId"].ToString());
                NovoItem.minReputation = Convert.ToInt32(Obj["minReputation"].ToString());
                //NovoItem.name = Obj["name"].ToString();
                //NovoItem.nameDescription = Obj["nameDescription"].ToString();
                //NovoItem.nameDescriptionColor = Obj["nameDescriptionColor"].ToString(); ;
                NovoItem.quality = Convert.ToInt32(Obj["quality"].ToString());
                //NovoItem.requiredLevel = Convert.ToInt32(Obj["requiredLevel"].ToString());
                //NovoItem.requiredSkill = Convert.ToInt32(Obj["requiredSkill"].ToString());
                //NovoItem.requiredSkillRank = Convert.ToInt32(Obj["requiredSkillRank"].ToString());
                //NovoItem.sellPrice = Convert.ToInt32(Obj["sellPrice"].ToString());
                NovoItem.stackable = Obj["stackable"].ToString();
                //NovoItem.upgradable = Convert.ToBoolean(Obj["upgradable"].ToString());

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

        private void GravaAtualizaItem(Item it)
        {
            BotWoWEntities n = new BotWoWEntities();

            try
            {
                Item c = (from p in n.Items where p.id == it.id select p).First();

                //NovoItem.armor = Convert.ToInt32(Obj["armor"].ToString());
                //NovoItem.baseArmor = Convert.ToInt32(Obj["baseArmor"].ToString());
                c.buyPrice = it.buyPrice;
                c.containerSlots = it.containerSlots;
                c.Desc = it.Desc;
                //NovoItem.disenchantingSkillRank = Convert.ToInt32(Obj["disenchantingSkillRank"].ToString());
                //NovoItem.displayInfoId = Convert.ToInt32(Obj["displayInfoId"].ToString());
                c.equippable = it.equippable;
                //NovoItem.hasSockets = Convert.ToBoolean(Obj["hasSockets"].ToString());
                //NovoItem.heroicTooltip = Convert.ToBoolean(Obj["heroicTooltip"].ToString());
                c.icon = it.icon;
                c.id = it.id;
                c.inventoryType = it.inventoryType;
                //NovoItem.isAuctionable = Convert.ToBoolean(Obj["isAuctionable"].ToString());
                c.itemBind = it.itemBind;
                c.itemClass = it.itemClass;
                c.itemLevel = it.itemLevel;
                c.itemSubClass = it.itemSubClass;
                c.maxCount = it.maxCount;
                c.maxDurability = it.maxDurability;
                c.minFactionId = it.minFactionId;
                c.minReputation = it.minReputation;
                //NovoItem.name = Obj["name"].ToString();
                //NovoItem.nameDescription = Obj["nameDescription"].ToString();
                //NovoItem.nameDescriptionColor = Obj["nameDescriptionColor"].ToString(); ;
                c.quality = it.quality;
                //NovoItem.requiredLevel = Convert.ToInt32(Obj["requiredLevel"].ToString());
                //NovoItem.requiredSkill = Convert.ToInt32(Obj["requiredSkill"].ToString());
                //NovoItem.requiredSkillRank = Convert.ToInt32(Obj["requiredSkillRank"].ToString());
                //NovoItem.sellPrice = Convert.ToInt32(Obj["sellPrice"].ToString());
                c.stackable = it.stackable;
                //NovoItem.upgradable = Convert.ToBoolean(Obj["upgradable"].ToString());

                n.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                n.Items.AddObject(it);
                n.SaveChanges();
            }

            catch (Exception ex)
            {
                throw new Exception("Erro na rotina GravaAtualizaItem", ex);
            }
        }

        public void AtualizarItem(string codigo)
        {

            Item i = RetornaItemBlizzard(codigo);
            GravaAtualizaItem(i);

        }

        public void AtualizaValorItem(decimal idItem, decimal valor)
        {

            BotWoWEntities n = new BotWoWEntities();
            Item i;

            i = (from p in n.Items where p.id == idItem select p).FirstOrDefault();

            if (i != null)
            {
                i.ValorMinnaAH = valor;
                n.SaveChanges();
            }


        }

    }
}