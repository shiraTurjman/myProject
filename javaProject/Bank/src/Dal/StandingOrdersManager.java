/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Dal;

import Entities.StandingOrders;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import javax.persistence.Query;

/**
 *
 * @author torge
 */
public class StandingOrdersManager extends GenericDataManager<StandingOrders>{

    public StandingOrdersManager() {
        super(StandingOrders.class);
    }
    
    public List<StandingOrders> getByEmailTo(String email)
    {
         entityManager.getTransaction().begin();
         Query q=entityManager.createNamedQuery("StandingOrders.findByEmailTo");
        q.setParameter("email", email);
        List<StandingOrders> sl=q.getResultList();
        
        entityManager.getTransaction().commit();
        return sl;
    }
    public List<StandingOrders> getByEmailFrom(String email)
    {
         entityManager.getTransaction().begin();
         Query q=entityManager.createNamedQuery("StandingOrders.findByEmailFrom");
        q.setParameter("email", email);
        List<StandingOrders> sl=q.getResultList();
        
        entityManager.getTransaction().commit();
        return sl;
    }
    
    public  Map<Integer,Double> ListToMap(List<StandingOrders> ls)
    {
        Map<Integer,Double> newMap=new HashMap<Integer,Double>();
        ls.forEach(s->{
            if(newMap.containsKey(s.getStandingOrdersPK().getDayInMonth()))
                
          newMap.put(s.getStandingOrdersPK().getDayInMonth(),newMap.get(s.getStandingOrdersPK().getDayInMonth())+s.getSumOfTransfer());
          else
            newMap.put(s.getStandingOrdersPK().getDayInMonth(),s.getSumOfTransfer());
        });
        
        return newMap;
    }
    
    
    
    
}
