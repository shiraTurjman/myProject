/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Entities;

import java.io.Serializable;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Embeddable;

/**
 *
 * @author torge
 */
@Embeddable
public class StandingOrdersPK implements Serializable {
    @Basic(optional = false)
    @Column(name = "ACCOUNT_NUM_FROM")
    private int accountNumFrom;
    @Basic(optional = false)
    @Column(name = "ACCOUNT_NUM_TO")
    private int accountNumTo;
    @Basic(optional = false)
    @Column(name = "DAY_IN_MONTH")
    private int dayInMonth;

    public StandingOrdersPK() {
    }

    public StandingOrdersPK(int accountNumFrom, int accountNumTo, int dayInMonth) {
        this.accountNumFrom = accountNumFrom;
        this.accountNumTo = accountNumTo;
        this.dayInMonth = dayInMonth;
    }

    public int getAccountNumFrom() {
        return accountNumFrom;
    }

    public void setAccountNumFrom(int accountNumFrom) {
        this.accountNumFrom = accountNumFrom;
    }

    public int getAccountNumTo() {
        return accountNumTo;
    }

    public void setAccountNumTo(int accountNumTo) {
        this.accountNumTo = accountNumTo;
    }

    public int getDayInMonth() {
        return dayInMonth;
    }

    public void setDayInMonth(int dayInMonth) {
        this.dayInMonth = dayInMonth;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (int) accountNumFrom;
        hash += (int) accountNumTo;
        hash += (int) dayInMonth;
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof StandingOrdersPK)) {
            return false;
        }
        StandingOrdersPK other = (StandingOrdersPK) object;
        if (this.accountNumFrom != other.accountNumFrom) {
            return false;
        }
        if (this.accountNumTo != other.accountNumTo) {
            return false;
        }
        if (this.dayInMonth != other.dayInMonth) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "Entities.StandingOrdersPK[ accountNumFrom=" + accountNumFrom + ", accountNumTo=" + accountNumTo + ", dayInMonth=" + dayInMonth + " ]";
    }
    
}
