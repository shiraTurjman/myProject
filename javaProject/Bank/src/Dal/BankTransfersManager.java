/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Dal;

import Entities.BankTransfers;
import Entities.Users;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.stream.Collectors;
import javax.persistence.Query;

/**
 *
 * @author torge
 */
public class BankTransfersManager extends GenericDataManager<BankTransfers>{

    public BankTransfersManager() {
        super(BankTransfers.class);
    }
    
    public  void add(BankTransfers bt)
    {
//         entityManager.getTransaction().begin();
//         Query q=entityManager.createNamedQuery("Users.findByTz");
//          q.setParameter("tz", bt.getUsers().getTz());
//          Users u=(Users)q.getSingleResult();
//          
//        entityManager.getTransaction().commit();
        
        if(bt.getUsers().getBalance()>bt.getSumOfTransfer())
        {
            super.add(bt);
            bt.getUsers().setBalance(bt.getUsers().getBalance()+bt.getSumOfTransfer());
            bt.getUsers1().setBalance(bt.getUsers1().getBalance()-bt.getSumOfTransfer());
        }
        
        
    }
    public List<BankTransfers> threeTransfer(String email)
    {
         entityManager.getTransaction().begin();
         Query q=entityManager.createNamedQuery("BankTransfers.find3day");
         Calendar cal=Calendar.getInstance();
         cal.add(Calendar.DATE,-3);
         Date before=cal.getTime();
         q.setParameter("before",before);
         
         List<BankTransfers> lb=q.getResultList();
         entityManager.getTransaction().commit();
         List<BankTransfers> lbNew=new ArrayList<BankTransfers>();
         
        for (BankTransfers lb1 : lb) {
            if(lb1.getUsers().getEmail().equals(email)||lb1.getUsers1().getEmail().equals(email))
            {
                lbNew.add(lb1);
            }
        }
        return lbNew;
         
    }
    
    public  List<BankTransfers> getby2bay(int num, Date d1,Date d2)
    {
         entityManager.getTransaction().begin();
         Query q=entityManager.createNamedQuery("BankTransfers.findAll");
         List<BankTransfers> lb=q.getResultList();
         entityManager.getTransaction().commit();
         List<BankTransfers> newList=lb.stream()
                 .filter(b-> (b.getUsers().getAccountNumber()==(num)||b.getUsers1().getAccountNumber().compareTo(num)==0)&&b.getBankTransfersPK().getDateOfTransfer().after(d1)&&b.getBankTransfersPK().getDateOfTransfer().before(d2)).distinct().collect(Collectors.toList());
    
         return newList;
    }
    
    
}
