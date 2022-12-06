/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Dal;

import Entities.Users;
import java.util.List;
import javax.persistence.Query;

/**
 *
 * @author torge
 */
public class UsersManager extends GenericDataManager<Users>{
    
    public UsersManager(){
     super(Users.class);
}
 
    public Users GetByEmailAndPassword(String email,String password)
    {
    entityManager.getTransaction().begin();
    Query q=entityManager.createNamedQuery("Users.findByEmailAndPassword");
    q.setParameter("password", password);
    q.setParameter("email", email);
    List<Users> u=q.getResultList();
    entityManager.getTransaction().commit();
    
   if(u==null|| u.isEmpty())
       return null;
   else
       return u.get(0);
    }
    public Users GetByAccountNamderAndPassword(int accountNamder,String password)
    {
     entityManager.getTransaction().begin();
    Query q=entityManager.createNamedQuery("Users.findByEmailAndPassword");
    q.setParameter("password", password);
    q.setParameter("accountNumber", accountNamder);
  List<Users> u=q.getResultList();
    entityManager.getTransaction().commit();
    
   if(u==null|| u.isEmpty())
       return null;
   else
       return u.get(0);
    
    }
    
}
