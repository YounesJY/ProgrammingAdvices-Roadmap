using PizzaProjectUpdated.Properties;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace PizzaProject
{
    public partial class frmPizzaProject : Form
    {
        private enum enPizzaSize : short
        {
            small = 20, medium = 30, large = 40
        };
        private enum enToppings : short
        {
            extraChees = 5, onion = 5, mushrooms = 5,
            olives = 5, tomatoes = 5, greenPappers = 5
        };
        private enum enPizzaCrustType : short
        {
            thin = 0, thick = 10
        };
        private enum enWhereToEat : short
        {
            eatIn = 1, takeOut = 2
        };



        private struct Toppings
        {
            public enToppings toppings;
            public bool isChecked;

            public Toppings(enToppings topping, bool isChecked)
            {
                this.toppings = topping;
                this.isChecked = isChecked;
            }
        }

        private Toppings[] _toppings = new Toppings[6];
        private enPizzaSize _pizzaSize = enPizzaSize.small;
        private enPizzaCrustType _pizzaCrustType = enPizzaCrustType.thin;
        private enWhereToEat _whereToEat = enWhereToEat.takeOut;

        public frmPizzaProject()
        {
            InitializeComponent();
        }
        private void PizzaProjectForm_Load(object sender, EventArgs e)
        {
            rbSizeMedium.Checked = true;
            rbCrustTypeThinCrust.Checked = true;
            rbWhereToEatTakeOut.Checked = true;

            chbToppingsExtraChees.Checked = false;
            chbToppingsOnion.Checked = false;
            chbToppingsMushRooms.Checked = false;
            chbToppingsOlives.Checked = false;
            chbToppingsTomatoes.Checked = false;
            chbToppingsGreenPapers.Checked = false;

            UpdateSummarySize();
            UpdateSummaryToppings();
            UpdateSummaryCrustType();
            UpdateSummaryWhereToEat();
            UpdateSummaryQuantity();
            UpdateSummaryPrice();
        }


        private short GetToppingsPrice()
        {
            short price = 0;

            foreach (Toppings item in _toppings)
            {
                if (item.isChecked)
                    price += Convert.ToInt16(item.toppings);
            }

            return price;
        }
        private short GetTotalPrice()
        {
            short singleUnitPrice = (byte)(Convert.ToInt16(_pizzaSize)
                + Convert.ToInt16(_pizzaCrustType)
                + GetToppingsPrice()
            );

            return (short)(nudQuantity.Value * singleUnitPrice);
        }
        private void ResetForm()
        {
            tcPizzaOptions.Enabled = true;

            rbSizeMedium.Checked = true;
            rbCrustTypeThinCrust.Checked = true;
            rbWhereToEatTakeOut.Checked = true;

            chbToppingsExtraChees.Checked = false;
            chbToppingsOnion.Checked = false;
            chbToppingsMushRooms.Checked = false;
            chbToppingsOlives.Checked = false;
            chbToppingsTomatoes.Checked = false;
            chbToppingsGreenPapers.Checked = false;

            nudQuantity.Value = 1;
            btnOperationsOrderPizza.Enabled = true;

            this.BackgroundImage = Resources.pizzaBackground;
        }


        // Summary Group
        private void UpdateSummarySize()
        {
            switch (_pizzaSize)
            {
                case enPizzaSize.small:
                    orderSummarySize.Text = "small";
                    break;
                case enPizzaSize.medium:
                    orderSummarySize.Text = "medium";
                    break;
                case enPizzaSize.large:
                    orderSummarySize.Text = "large";
                    break;
                default:
                    orderSummarySize.Text = "small";
                    break;
            }
        }
        private void UpdateSummaryToppings()
        {

            string toppings = "";
            string[] toppingsArray = { "Extra Chees", ", Onion", ", Mushrooms", ", Olives", ", Tomatoes", ", Green Pappers" };

            for (int i = 0; i < _toppings.Length; i++)
            {
                if (_toppings[i].isChecked)
                    toppings += toppingsArray[i];
            }

            orderSummaryToppings.Text = (toppings.Equals("") ? "No Toppings" : toppings);
            orderSummaryToppings.Text = (toppings.StartsWith(", ") ? toppings.Substring(2, toppings.Length - 2) : toppings);

        }
        private void UpdateSummaryCrustType()
        {
            switch (_pizzaCrustType)
            {
                case enPizzaCrustType.thin:
                    orderSummaryCrustType.Text = "thin";
                    break;
                case enPizzaCrustType.thick:
                    orderSummaryCrustType.Text = "thick";
                    break;
                default:
                    orderSummaryCrustType.Text = "thin";
                    break;
            }
        }
        private void UpdateSummaryWhereToEat()
        {
            switch (_whereToEat)
            {
                case enWhereToEat.eatIn:
                    orderSummaryWhereToEat.Text = "Eat In";
                    break;
                case enWhereToEat.takeOut:
                    orderSummaryWhereToEat.Text = "Take Out";
                    break;
                default:
                    orderSummaryWhereToEat.Text = "Take Out";
                    break;
            }
        }
        private void UpdateSummaryQuantity()
        {
            orderSummaryQuantity.Text = nudQuantity.Value.ToString();
        }
        private void UpdateSummaryPrice()
        {
            txtPrice.Text = GetTotalPrice() + " $";
        }


        // Size Group
        private void rbSizeSmall_CheckedChanged(object sender, EventArgs e)
        {
            _pizzaSize = enPizzaSize.small;
            UpdateSummarySize();
            UpdateSummaryPrice();
        }
        private void rbSizeMedium_CheckedChanged(object sender, EventArgs e)
        {
            _pizzaSize = enPizzaSize.medium;
            UpdateSummarySize();
            UpdateSummaryPrice();
        }
        private void rbSizeLarge_CheckedChanged(object sender, EventArgs e)
        {
            _pizzaSize = enPizzaSize.large;
            UpdateSummarySize();
            UpdateSummaryPrice();
        }


        // Toppings Group
        private void chbToppingsExtraChees_CheckedChanged(object sender, EventArgs e)
        {
            _toppings[0] = new Toppings(enToppings.extraChees, chbToppingsExtraChees.Checked);
            UpdateSummaryToppings();
            UpdateSummaryPrice();
        }
        private void chbToppingsOnion_CheckedChanged(object sender, EventArgs e)
        {
            _toppings[1] = new Toppings(enToppings.onion, chbToppingsOnion.Checked);
            UpdateSummaryToppings();
            UpdateSummaryPrice();
        }
        private void chbToppingsMushRooms_CheckedChanged(object sender, EventArgs e)
        {
            _toppings[2] = new Toppings(enToppings.mushrooms, chbToppingsMushRooms.Checked);
            UpdateSummaryToppings();
            UpdateSummaryPrice();
        }
        private void chbToppingsOlives_CheckedChanged(object sender, EventArgs e)
        {
            _toppings[3] = new Toppings(enToppings.olives, chbToppingsOlives.Checked);
            UpdateSummaryToppings();
            UpdateSummaryPrice();
        }
        private void chbToppingsTomatoes_CheckedChanged(object sender, EventArgs e)
        {
            _toppings[4] = new Toppings(enToppings.tomatoes, chbToppingsTomatoes.Checked);
            UpdateSummaryToppings();
            UpdateSummaryPrice();
        }
        private void chbToppingsGreenPapers_CheckedChanged(object sender, EventArgs e)
        {
            _toppings[5] = new Toppings(enToppings.greenPappers, chbToppingsGreenPapers.Checked);
            UpdateSummaryToppings();
            UpdateSummaryPrice();
        }


        // Crust Type Group
        private void rbCrustTypeThinCrust_CheckedChanged(object sender, EventArgs e)
        {
            _pizzaCrustType = enPizzaCrustType.thin;
            UpdateSummaryCrustType();
            UpdateSummaryPrice();
        }
        private void rbCrustTypeThinkCrust_CheckedChanged(object sender, EventArgs e)
        {
            _pizzaCrustType = enPizzaCrustType.thick;
            UpdateSummaryCrustType();
            UpdateSummaryPrice();
        }


        // Where To Eat Group
        private void rbWhereToEatIn_CheckedChanged(object sender, EventArgs e)
        {
            _whereToEat = enWhereToEat.eatIn;
            UpdateSummaryWhereToEat();
        }
        private void rbWhereToEatTakeOut_CheckedChanged(object sender, EventArgs e)
        {
            _whereToEat = enWhereToEat.takeOut;
            UpdateSummaryWhereToEat();
        }


        // Quantity Group
        private void nudQuantity_Scroll(object sender, EventArgs e)
        {
            UpdateSummaryQuantity();
            UpdateSummaryPrice();
        }
        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            UpdateSummaryQuantity();
            UpdateSummaryPrice();
        }


        // Operations Group
        private void btnOperationsOrderPizza_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm order ", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                MessageBox.Show("Order Placed Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tcPizzaOptions.Enabled = false;
                btnOperationsOrderPizza.Enabled = false;
            }

            btnOperationsReset.Focus();
        }
        private void btnOperationsReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnChangeBackground_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = @"C:\Users\poste\Donwloads";
            openFileDialog.Title = "Choose the new background image";

            openFileDialog.DefaultExt = "png";
            openFileDialog.Filter = "image files (.png, .jpge ....) | *.jpg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show($"The new background will be set to : {openFileDialog.FileName}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.BackgroundImage = Image.FromFile(openFileDialog.FileName);
            }
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiChangeFont_Click(object sender, EventArgs e)
        {
            ProjectLabel.Font = fontDialog.Font;
            ProjectLabel.ForeColor = fontDialog.Color;
        }

        private void tsmiFormChangeBackground_Click(object sender, EventArgs e)
        {
            btnChangeBackground_Click(sender, e);
        }

        private void tsmiFormClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
