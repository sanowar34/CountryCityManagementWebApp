﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CountryCityManagementWebApp.BLL;
using CountryCityManagementWebApp.Models;

namespace CountryCityManagementWebApp
{
    public partial class CityEntryUI : System.Web.UI.Page
    {
        Manager manager = new Manager();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                countryDropDownList.DataSource = manager.GetAllCountries();
                countryDropDownList.DataTextField = "CountryName";
                countryDropDownList.DataValueField = "CountryID";
                countryDropDownList.DataBind();
                countryDropDownList.Items.Insert(0, new ListItem("---Select Country---"));
            }

            LoadAllCities();
        }

        private void LoadAllCities()
        {
            List<City> cityList = manager.GetAllCities();
            LoadCities(cityList);
        }

        private void LoadCities(List<City> cityList)
        {
            cityGridView.DataSource = cityList;
            cityGridView.DataBind();
        }


        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (cityNameTextBox.Text.Length >= 1)
            {
                if( countryDropDownList.SelectedIndex>0)
                {
                    if (noOfDwellersTextBox.Text.Length > 0)
                    {
                        int countryID = Convert.ToInt32(countryDropDownList.SelectedValue);


                        var city = new City(cityNameTextBox.Text, cityAboutTextBox.Text,
                            Convert.ToInt32(noOfDwellersTextBox.Text), cityLocationTextBox.Text, cityWeatherTextBox.Text);

                        city.CountryID = countryID;

                        showMessageLable.Text = manager.AddCity(city);
                        LoadAllCities();
                    }

                    else
                    {
                        showMessageLable.Text = "Enter No Of Dwellers";
                    }
                    
                }
            else
                {
                    showMessageLable.Text = "Select Country";
                }
            }
            else
            {
                showMessageLable.Text = "Enter City name.";
            }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cityGridView.PageIndex = e.NewPageIndex;
            LoadAllCities();
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CityEntryUI.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblSerial");
                lblSerial.Text = ((cityGridView.PageIndex * cityGridView.PageSize) + e.Row.RowIndex + 1).ToString();
            }
        }

    }
}
