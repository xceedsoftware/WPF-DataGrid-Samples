
<!--
tags: Xceed DataGrid for WPF, WPF Data Grid, .NET DataGrid, Data Binding, DataGrid Examples, High-Performance Data Grid, WPF Controls, Xceed Software, Data Virtualization, DataGrid Customization, Data Exporting, MVVM, DataGrid Themes, DataGrid Styling
-->

![NuGet Downloads](https://img.shields.io/nuget/dt/Xceed.Products.Wpf.DataGrid.Full) ![Static Badge](https://img.shields.io/badge/.Net_Framework-4.0%2B-blue) ![Static Badge](https://img.shields.io/badge/.Net-5.0%2B-blue) [![Learn More](https://img.shields.io/badge/Learn-More-blue?style=flat&labelColor=gray)](https://xceed.com/en/our-products/product/datagrid-for-wpf)

[![Xceed DataGrid for WPF](./Resources/header.png)](https://xceed.com/en/our-products/product/datagrid-for-wpf)

# Xceed DataGrid for WPF - Examples

This repository contains examples demonstrating how to use the Xceed DataGrid for WPF. The examples are provided in both C# and Visual Basic .NET.

## About The Product

The Xceed DataGrid for WPF is a feature-rich and efficient data grid control for the Windows Presentation Foundation (WPF) framework. Designed to meet the needs of today's developers, it offers a wide range of capabilities to create visually appealing, high-performance, and flexible data grids. Key features include:

- **Performance**: Optimized for fast, responsive data display and handling large datasets.
- **Themes and Styling**: Includes various themes and styling options to seamlessly integrate with your application's look and feel.
- **Data Binding**: Supports a variety of data sources, including asynchronous binding.
- **Editing**: Comprehensive editing capabilities, including multiple edit modes and built-in editors.
- **Exporting**: Export data to popular formats such as Excel and CSV.
- **Customizability**: Highly customizable with flexible row and column configurations, custom views, and more.
- **Advanced Features**: Includes advanced features like data virtualization, grouping, summaries, and more.

For more information, please visit the [official product page](https://xceed.com/en/our-products/product/datagrid-for-wpf).

## Getting Started

### 1. Installing the DataGrid from nuget
To install the Xceed DataGrid for WPF from NuGet, follow these steps:

1. **Open your project in Visual Studio.**
2. **Open the NuGet Package Manager Console** by navigating to `Tools > NuGet Package Manager > Package Manager Console`.
3. **Run the following command:**
```sh
   dotnet add package Xceed.Products.Wpf.DataGrid.Full
```

4. Alternatively, you can use the NuGet Package Manager GUI:

1. Right-click on your project in the Solution Explorer.
2. Select Manage NuGet Packages.
3. Search for Xceed.Products.Wpf.DataGrid.Full and click Install.

### 2. Adding a DataGrid to the XAML

To add a DataGrid to your XAML, follow these steps:

1. **Open your XAML file (e.g., MainWindow.xaml).**
2. **Add the following namespace at the top of your XAML file:**
   ```xaml
   xmlns:xcdg="http://schemas.xceed.com/wpf/xaml/datagrid"
   ```
3. **Add the DataGrid control to your layout:**
   ```xaml
   <xcdg:DataGridControl x:Name="myDataGrid"
                         AutoCreateColumns="True"
                         ItemsSource="{Binding YourDataSource}" />
   ```
4. Ensure your DataContext is set to an appropriate data source in your code-behind or ViewModel.

### 3. How to License the Product Using the LicenseKey Property
To license the Xceed DataGrid for WPF using the LicenseKey property, follow these steps:

1. **Obtain your license key** from Xceed. (Download the product from xceed.com or send us a request at support@xceed.com
2. **Set the LicenseKey property in your application startup code:**
   ```csharp
   using System.Windows;

   public partial class MainWindow : Window
   {
       public MainWindow()
       {
           InitializeComponent();
           Xceed.Wpf.DataGrid.Licenser.LicenseKey = "Your-Key-Here";
       }
   }
   ```
3. Ensure the license key is set before any DataGrid control is instantiated.

## Examples Overview

Below is a list of the examples available in this repository:

- **AsyncBinding**: Demonstrates how to bind the DataGrid asynchronously.
- **BatchUpdating**: Shows how to perform batch updates in the DataGrid.
- **CardView**: Provides an example of displaying data in a card view layout.
- **ColumnChooser**: Demonstrates how to implement a column chooser for the DataGrid.
- **ColumnManagerRow**: Shows how to use a column manager row.
- **CustomFiltering**: Demonstrates custom filtering techniques.
- **CustomViews**: Provides examples of custom views in the DataGrid.
- **DataVirtualization**: Shows how to use data virtualization to enhance performance.
- **EditModes**: Demonstrates various edit modes available in the DataGrid.
- **Exporting**: Provides examples of exporting data to different formats.
- **FlexibleBinding**: Shows how to bind data flexibly.
- **FlexibleRowsColumn**: Demonstrates flexible row and column configurations.
- **Formatting**: Provides examples of data formatting.
- **Grouping**: Demonstrates grouping data in the DataGrid.
- **IncludedEditors**: Shows how to use included editors.
- **LargeDataSets**: Demonstrates handling large datasets.
- **LiveUpdating**: Shows how to update data live.
- **MasterDetail**: Demonstrates master-detail views.
- **MergedHeaders**: Shows how to create merged headers.
- **MultiView**: Demonstrates multiple view configurations.
- **MVVM**: Provides examples of using MVVM pattern with the DataGrid.
- **PersistSettings**: Shows how to persist settings.
- **Printing**: Demonstrates printing capabilities.
- **Selection**: Shows how to handle selection in the DataGrid.
- **SpannedCells**: Demonstrates cell spanning techniques.
- **SummariesAndTotals**: Shows how to implement summaries and totals.
- **Tableflow**: Demonstrates table flow layout.
- **TableView**: Shows how to use the table view.
- **Theming**: Demonstrates theming capabilities.
- **TreeGridflowView**: Shows how to implement a tree grid flow view.
- **Validation**: Demonstrates data validation techniques.
- **Views3D**: Provides examples of 3D views.

## Getting Started

To get started with these examples, clone the repository and open the solution file in Visual Studio.

```bash
git clone https://github.com/your-repo/xceed-datagrid-wpf-examples.git
cd xceed-datagrid-wpf-examples
```
Open the solution file in Visual Studio and build the project to restore the necessary NuGet packages.

## Requirements
- Visual Studio 2015 or later
- .NET Framework 4.0 or later
- .NET 5.0 or later
  
## Documentation

For more information on how to use the Xceed DataGrid for WPF, please refer to the [official documentation](https://doc.xceed.com/xceed-datagrid-for-wpf/webframe.html#rootWelcome.html).

## Licensing

To receive a license key, visit [xceed.com](https://xceed.com) and download the product, or contact us directly at [support@xceed.com](mailto:support@xceed.com) and we will provide you with a trial key.

## Contact

If you have any questions, feel free to open an issue or contact us at [support@xceed.com](mailto:support@xceed.com).

---

© 2024 Xceed Software Inc. All rights reserved.
