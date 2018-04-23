using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;

namespace SilverlightApplication1 {
    public partial class MainPage : UserControl, IDisposable {
        Session session = null;
        public MainPage() {
            InitializeComponent();
            ThreadPool.QueueUserWorkItem(o => {
                XpoDefault.DataLayer = XpoDefault.GetDataLayer(
                   "http://localhost:64466/Service1.svc",
                   AutoCreateOption.SchemaAlreadyExists
               );
                session = new Session();
                //It is necessary to call UpdataSchema method for all persistent classes.
                session.UpdateSchema(typeof(Customer));
                if(session.FindObject(typeof(Customer), new BinaryOperator("ContactName", "Alex Smith", BinaryOperatorType.Equal)) == null) {
                    Customer custAlex = new Customer(session) { ContactName = "Alex Smith", CompanyName = "DevExpress" };
                    custAlex.Save();
                    Customer Tom = new Customer(session) { ContactName = "Tom Jensen", CompanyName = "ExpressIT" };
                    Tom.Save();
                }
                session.TypesManager.EnsureIsTypedObjectValid();
                Dispatcher.BeginInvoke(BeginInitializeDataSource);
            });
        }
        void BeginInitializeDataSource() {
            var query = from c in session.Query<Customer>()
                        select c;
            //Execute the query asynchronously.
            query.EnumerateAsync(EndInitializeDataSource);

        }
        void EndInitializeDataSource(IEnumerable<Customer> result, Exception ex) {
            //Assign the data source to the control.
            gridControl1.ItemsSource = result;
        }
        public void Dispose() {
            if(session != null) {
                session.Dispose();
                session = null;
            }
        }
    }
}
