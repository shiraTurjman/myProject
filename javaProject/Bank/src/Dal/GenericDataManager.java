/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Dal;

import Entities.Users;
import Entities.BankTransfers;
import Entities.StandingOrders;
import java.util.List;
import static javafx.scene.input.KeyCode.T;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import javax.persistence.Query;

/**
 *
 * @author torge
 */

public class GenericDataManager <T> {
    
     public EntityManagerFactory entityManagerFactory;
     public EntityManager entityManager;
     
     private Class MyClass; 
    public GenericDataManager(Class c){
        entityManagerFactory=Persistence.createEntityManagerFactory("BankPU");
        entityManager=entityManagerFactory.createEntityManager();
        MyClass=c;
    }
    
    public void add(T p){
        entityManager.getTransaction().begin();
        entityManager.persist(p);
        entityManager.getTransaction().commit();
    }
    public T delete(Object pk){
        
         entityManager.getTransaction().begin();
        T itemToDelete=(T)entityManager.find(MyClass,pk);
        entityManager.remove(itemToDelete);
        entityManager.getTransaction().commit();
        return itemToDelete;
    }
    
    public void update(T obj){
     entityManager.getTransaction().begin();
      if(entityManager.contains(obj))
      {    entityManager.merge(obj);}
      else{
          throw new RuntimeException("the obj not fined");}
      entityManager.getTransaction().commit();
    }
    
    public T findById(Object id){
    
         entityManager.getTransaction().begin();
        T itemToFine=(T)entityManager.find(MyClass,id);
  
        entityManager.getTransaction().commit();
        return itemToFine;
    }
    
    
    
    public void close(){
    entityManager.close();
    entityManagerFactory.close();
    }
}
