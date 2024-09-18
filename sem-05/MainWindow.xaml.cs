using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sem_05
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void botonListaCliente_Click(object sender, RoutedEventArgs e)
        {
            ListData();
        }

        private void ListData(string filter = "")
        {
            try
            {
                //"Data Source=DESKTOP-F970KVM;Initial Catalog=FacturaDB;User ID=usrFactura;Password=123456"
                string cadena = "Data Source=DESKTOP-MJBK07S;Initial Catalog=Neptuno2;User ID=userNeptuno;Password=123456";
                using SqlConnection conect = new SqlConnection(cadena);

                conect.Open();

                SqlCommand comand = new SqlCommand("USP_ListarClientes", conect);
                comand.CommandType = CommandType.StoredProcedure;

                List<Cliente> listaClientes = new List<Cliente>();
                SqlDataReader reader = comand.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.idCliente = reader["idCliente"];
                    cliente.NombreCompañia = reader["NombreCompañia"].ToString();
                    cliente.NombreContacto = reader["NombreContacto"].ToString();
                    cliente.CargoContacto = reader["CargoContacto"].ToString();
                    cliente.Ciudad = reader["Ciudad"].ToString();
                    listaClientes.Add(cliente);
                }
                conect.Close();

                listadoClientes.DataSource = listaClientes;

            }
            catch (Exception e)
            {
                MessageBox.Show($"Error al cargar los productos: {e.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}