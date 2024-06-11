/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [RelayCommand.cs]
 *  
 * This class implements the ICommand interface, which is then used in the ViewModel to create commands that are consumed by the View.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Windows.Input;

namespace Xceed.Wpf.DataGrid.Samples.MVVM.ViewModel
{
  public class RelayCommand : ICommand
  {
    private readonly Predicate<object> m_canExecute;
    private readonly Action<object> m_execute;

    public RelayCommand( Action<object> execute )
      : this( execute, null )
    {
    }

    public RelayCommand( Action<object> execute, Predicate<object> canExecute )
    {
      if( execute == null )
        throw new ArgumentNullException( "execute" );

      m_execute = execute;
      m_canExecute = canExecute;
    }

    #region ICommand Members

    event EventHandler ICommand.CanExecuteChanged
    {
      add
      {
        CommandManager.RequerySuggested += value;
      }

      remove
      {
        CommandManager.RequerySuggested -= value;
      }
    }

    bool ICommand.CanExecute( object parameter )
    {
      return m_canExecute != null ? m_canExecute( parameter ) : true;
    }

    void ICommand.Execute( object parameter )
    {
      m_execute( parameter );
    }

    #endregion
  }
}
