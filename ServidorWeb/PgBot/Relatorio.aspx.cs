using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServidorWeb.Estruturas;
using ServidorWeb.BD;

namespace ServidorWeb
{
    public partial class Relatorio : System.Web.UI.Page
    {

        BotWoWEntities b = EntityContextBot.GetContext;
        
        protected int[] ItensRelatorio()
        {
             int[] ids = { 72092, 72094, 72103, 72238, 79011, 79010, 72235, 72234, 72237 };
             return ids;
        }

        protected Dictionary<Int32, Int32> NovoDicionario()
        {

            Dictionary<Int32, Int32> lt = new Dictionary<Int32, Int32>();

            int[] it = ItensRelatorio();

            foreach (int item in it)
            {
                lt.Add(item, 0);
            }

            return lt;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                DropDownList1.Items.Clear();
                
                var nomes = b.Sessaos.Select(m => m.nome).Distinct();

                foreach (string item in nomes)
                {
                    DropDownList1.Items.Add(new ListItem(item));
                }

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            

            //            IQueryable teste = b.Sessaos.GroupBy(p => p.dt_inicio).Select(g => new { dt_inicio = g.Key, count = g.Count() });

            string nome;
            nome = DropDownList1.SelectedItem.ToString();

            ListBox1.Items.Clear();
            ListBox2.Items.Clear();

            Label1.Text = Calendar1.SelectedDate.ToString();

            var tempo = Sessoes(nome, Calendar1.SelectedDate, Calendar2.SelectedDate);

            foreach (Sessao item in tempo)
            {

                if (item.dt_fim == null)
                {
                    ListBox1.Items.Add(new ListItem(item.dt_inicio + " Sem final"));
                }
                else
                {
                    TimeSpan t = ((DateTime)item.dt_fim).Subtract(item.dt_inicio);
                    DateTime periodo = new DateTime(t.Ticks);

                    ListBox1.Items.Add(new ListItem(String.Format("Data Inicial: {3}, Data Final: {4} Horas: {0}  Minutos: {1}  Segundos: {2} \r\n", periodo.Hour, periodo.Minute, periodo.Second, item.dt_inicio, item.dt_fim)));
                }

            }

            var l = Loot(tempo, ItensRelatorio());
            var m = ConverteDicionario(l);
            foreach (var il in m)
            {
                if (il.Value > 0)
                {
                    ListBox2.Items.Add(new ListItem(String.Format("Item id: {0} qtd: {1}",((estItem)il.Key).nome, il.Value)));
                }
            }




        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected System.Collections.Generic.IEnumerable<Sessao> Sessoes(string nome)
        {

            var result = (from p in b.Sessaos where p.nome == nome select p);

            return result;

        }
        protected System.Collections.Generic.IEnumerable<Sessao> Sessoes(string nome, DateTime dtInicio)
        {
            var result = (from p in b.Sessaos where p.nome == nome && p.dt_inicio >= dtInicio select p);

            return result;

        }
        protected System.Collections.Generic.IEnumerable<Sessao> Sessoes(string nome, DateTime dtInicio, DateTime dtFim)
        {
            var result = (from p in b.Sessaos where p.nome == nome && p.dt_inicio >= dtInicio && p.dt_inicio <= dtFim select p);

            return result;

        }

        protected Dictionary<Int32, Int32> Loot(Sessao s, int[] itens)
        {

            var lt = NovoDicionario();

            var loot = (from p in b.loots where p.id_sessao == s.id && itens.Contains((Int32)p.id_Item) select p);

                foreach (loot iloot in loot)
                {

                    lt[(Int32)iloot.id_Item] = lt[(Int32)iloot.id_Item] + (Int32)iloot.qtd;

                }
        
            return lt;

        }
        protected Dictionary<Int32, Int32> Loot(System.Collections.Generic.IEnumerable<Sessao> s, int[] itens)
        {

            var lt = NovoDicionario();

            var loot = (from p in b.loots where s.Contains(p.Sessao) && itens.Contains((Int32)p.id_Item) select p);

            foreach (loot iloot in loot)
            {

                lt[(Int32)iloot.id_Item] = lt[(Int32)iloot.id_Item] + (Int32)iloot.qtd;

            }

            return lt;
        }

        protected Dictionary<estItem, Int32> ConverteDicionario(Dictionary<Int32, Int32> d)
        {

            Dictionary<estItem, Int32> resultado = new Dictionary<estItem, int>();

            foreach (var item in d)
            {
                resultado.Add(RetornaItem(item.Key), item.Value);
            }

            return resultado;
        }
        
        protected estItem RetornaItem(Int32 id)
        {
            estItem i = new estItem();
            
            switch (id)
            {
                case 72092:
                    i.id = id;
                    i.nome = "Ghost Iron Ore";
                    break;
                case 72094:
                    i.id = id;
                    i.nome = "Black Trillium Ore";
                    break;
                case 72103:
                    i.id = id;
                    i.nome = "White Trillium Ore";
                    break;
                case 72238:
                    i.id = id;
                    i.nome = "Golden Lotus";
                    break;
                case 79011:
                    i.id = id;
                    i.nome = "Fool's Cap";
                    break;
                case 79010:
                    i.id = id;
                    i.nome = "Snow Lily";
                    break;
                case 72235:
                    i.id = id;
                    i.nome = "Silkweed";
                    break;
                case 72234:
                    i.id = id;
                    i.nome = "Green Tea Leaf";
                    break;
                case 72237:
                    i.id = id;
                    i.nome = "Rain Poppy";
                    break;

                default:
                    break;
            }

            return i;
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}