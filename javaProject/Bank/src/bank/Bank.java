/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package bank;

import Dal.BankTransfersManager;
import Dal.UsersManager;
import Entities.Users;


/**
 *
 * @author torge
 */
public class Bank {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
        System.out.println("qqq");
        UsersManager um=new UsersManager();
        Users u= um.GetByEmailAndPassword("ghnkj@ghj","12234");
//        
        System.out.println(u.getEmail());
        
    }
    
}
