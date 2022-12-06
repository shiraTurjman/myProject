/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Entities;

import java.io.Serializable;
import javax.persistence.Column;
import javax.persistence.EmbeddedId;
import javax.persistence.Entity;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;
import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author torge
 */
@Entity
@Table(name = "STANDING_ORDERS")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "StandingOrders.findAll", query = "SELECT s FROM StandingOrders s"),
    @NamedQuery(name = "StandingOrders.findByAccountNumFrom", query = "SELECT s FROM StandingOrders s WHERE s.standingOrdersPK.accountNumFrom = :accountNumFrom"),
    @NamedQuery(name = "StandingOrders.findByAccountNumTo", query = "SELECT s FROM StandingOrders s WHERE s.standingOrdersPK.accountNumTo = :accountNumTo"),
 
    @NamedQuery(name = "StandingOrders.findByDayInMonth", query = "SELECT s FROM StandingOrders s WHERE s.standingOrdersPK.dayInMonth = :dayInMonth"),
     @NamedQuery(name = "StandingOrders.findByEmailFrom", query = "SELECT s FROM StandingOrders s WHERE s.users.email = :email"),
    @NamedQuery(name = "StandingOrders.findByEmailTo", query = "SELECT s FROM StandingOrders s WHERE s.users1.email = :email"),
    
    @NamedQuery(name = "StandingOrders.findBySumOfTransfer", query = "SELECT s FROM StandingOrders s WHERE s.sumOfTransfer = :sumOfTransfer")})
public class StandingOrders implements Serializable {
    private static final long serialVersionUID = 1L;
    @EmbeddedId
    protected StandingOrdersPK standingOrdersPK;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Column(name = "SUM_OF_TRANSFER")
    private Double sumOfTransfer;
    @JoinColumn(name = "ACCOUNT_NUM_TO", referencedColumnName = "ACCOUNT_NUMBER", insertable = false, updatable = false)
    @ManyToOne(optional = false)
    private Users users;
    @JoinColumn(name = "ACCOUNT_NUM_FROM", referencedColumnName = "ACCOUNT_NUMBER", insertable = false, updatable = false)
    @ManyToOne(optional = false)
    private Users users1;

    public StandingOrders() {
    }

    public StandingOrders(StandingOrdersPK standingOrdersPK) {
        this.standingOrdersPK = standingOrdersPK;
    }

    public StandingOrders(int accountNumFrom, int accountNumTo, int dayInMonth) {
        this.standingOrdersPK = new StandingOrdersPK(accountNumFrom, accountNumTo, dayInMonth);
    }

    public StandingOrdersPK getStandingOrdersPK() {
        return standingOrdersPK;
    }

    public void setStandingOrdersPK(StandingOrdersPK standingOrdersPK) {
        this.standingOrdersPK = standingOrdersPK;
    }

    public Double getSumOfTransfer() {
        return sumOfTransfer;
    }

    public void setSumOfTransfer(Double sumOfTransfer) {
        this.sumOfTransfer = sumOfTransfer;
    }

    public Users getUsers() {
        return users;
    }

    public void setUsers(Users users) {
        this.users = users;
    }

    public Users getUsers1() {
        return users1;
    }

    public void setUsers1(Users users1) {
        this.users1 = users1;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (standingOrdersPK != null ? standingOrdersPK.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof StandingOrders)) {
            return false;
        }
        StandingOrders other = (StandingOrders) object;
        if ((this.standingOrdersPK == null && other.standingOrdersPK != null) || (this.standingOrdersPK != null && !this.standingOrdersPK.equals(other.standingOrdersPK))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "Entities.StandingOrders[ standingOrdersPK=" + standingOrdersPK + " ]";
    }
    
}
