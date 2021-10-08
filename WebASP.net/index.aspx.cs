using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebASP.net
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            panOrder.Visible = false;
            panPricing.Visible = false;

            if(!Page.IsPostBack)
            {
                txtAddress.Visible = lblAddress.Visible = false;
                //
                //Add option to select pizza at position 0
                //Vegetarian is at position 0
                cboPizza.Items.Add(new ListItem("Select a Piiza"));
                cboPizza.Items.Add(new ListItem("Vegetarian", "10"));
                cboPizza.Items.Add(new ListItem("Hawaiian", "12"));
                cboPizza.Items.Add(new ListItem("All Dressed", "8"));
                cboPizza.Items.Add(new ListItem("Halal", "20"));
                //
                lstPizzaSize.Items.Add(new ListItem("Small", "1"));
                lstPizzaSize.Items.Add(new ListItem("Medium", "1.5"));
                lstPizzaSize.Items.Add(new ListItem("Large", "2"));
                //
                lstPizzaSize.SelectedIndex = 0;
                //
                chkLstToppings.Items.Add(new ListItem("Bacon", "3"));
                chkLstToppings.Items.Add(new ListItem("Mushroom", "1.5"));
                chkLstToppings.Items.Add(new ListItem("Extra Cheese", "2"));
                chkLstToppings.Items.Add(new ListItem("Olives", "2.5"));
                //
                radlstCrust.Items.Add(new ListItem("Normal"));
                radlstCrust.Items.Add(new ListItem("Thin"));
                radlstCrust.Items.Add(new ListItem("Thick"));
                //
                radlstCrust.SelectedIndex = 0;

            }

            if(cboPizza.SelectedIndex > 0)
            {
                calculatePrice();
            }

        }

        private void calculatePrice()
        {
            decimal baseP = 0;
            decimal delivery = 0;
            decimal total = 0;
            decimal toppings = 0, subtotal=0, taxes=0;

            baseP = Convert.ToDecimal(cboPizza.SelectedItem.Value) * Convert.ToDecimal(lstPizzaSize.SelectedItem.Value);

            litPricing.Text = "<br>Base Price: $ " + baseP + "</br>";
            if(chkDeliver.Checked)
            {
                delivery = 3;
                litPricing.Text += "Delivery Price: $ " + delivery + "</br>";
            }
            
            foreach (ListItem item in chkLstToppings.Items)
            {
                //If any item is added, add value to toppings, else add 0
                toppings += (item.Selected) ? Convert.ToDecimal(item.Value) : 0;
            }

            litPricing.Text += "Toppings Price : $ " + toppings + "</br>";

            subtotal = baseP + delivery + toppings;

            litPricing.Text += "SubTotal Price : $ " + subtotal + "</br>";

            taxes += (subtotal * Convert.ToDecimal(0.15));

            litPricing.Text += "Taxes Price : $ " + taxes + "</br>";

            total = Math.Round(subtotal + taxes,2);

            litPricing.Text += "Total Price : $ " + total + "</br>";


            panPricing.Visible = true;
        }

        protected void chkDeliver_CheckedChanged(object sender, EventArgs e)
        {
            lblAddress.Visible = txtAddress.Visible = chkDeliver.Checked;
            //txtAddress.Visible = lblAddress.Visible = (chkDelivery.Checked)
        }

        protected void cboPizza_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnOrderNow_Click(object sender, EventArgs e)
        {
            panOrder.Visible = true;

            litOrder.Text += "Sir/Miss " + txtCustomer.Text + ",<br> Your order for a " + lstPizzaSize.SelectedItem.Text + "<br>" + cboPizza.SelectedItem.Text + " Pizza,<br> with toppings : <ul>";

            foreach(ListItem item in chkLstToppings.Items)
            {
                litOrder.Text += item.Selected ? "<li>" + item.Text + "</li>" : "";
            }
            litOrder.Text += "</ul> <br> On a " + radlstCrust.SelectedItem.Text + " crust <br>.";

            litOrder.Text += (chkDeliver.Checked) ? "The Pizza will be delivered to " + txtAddress.Text : "The Pizza is for Pick Up.";
        }
    }
}