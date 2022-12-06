/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Entities;

import java.io.Serializable;
import java.util.Date;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Embeddable;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

/**
 *
 * @author torge
 */
@Embeddable
public class BankTransfersPK implements Serializable {
    @Basic(optional = false)
    @Column(name = "ACCOUNT_NUM_FROM")
    private int accountNumFrom;
    @Basic(optional = false)
    @Column(name = "ACCOUNT_NUM_TO")
    private int accountNumTo;
    @Basic(optional = false)
    @Column(name = "DATE_OF_TRANSFER")
    @Temporal(TemporalType.TIMESTAMP)
    private Date dateOfTransfer;

    public BankTransfersPK() {
    }

    public BankTransfersPK(int accountNumFrom, int accountNumTo, Date dateOfTransfer) {
        this.accountNumFrom = accountNumFrom;
        this.accountNumTo = accountNumTo;
        this.dateOfTransfer = dateOfTransfer;
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

    public Date getDateOfTransfer() {
        return dateOfTransfer;
    }

    public void setDateOfTransfer(Date dateOfTransfer) {
        this.dateOfTransfer = dateOfTransfer;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (int) accountNumFrom;
        hash += (int) accountNumTo;
        hash += (dateOfTransfer != null ? dateOfTransfer.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof BankTransfersPK)) {
            return false;
        }
        BankTransfersPK other = (BankTransfersPK) object;
        if (this.accountNumFrom != other.accountNumFrom) {
            return false;
        }
        if (this.accountNumTo != other.accountNumTo) {
            return false;
        }
        if ((this.dateOfTransfer == null && other.dateOfTransfer != null) || (this.dateOfTransfer != null && !this.dateOfTransfer.equals(other.dateOfTransfer))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "Entities.BankTransfersPK[ accountNumFrom=" + accountNumFrom + ", accountNumTo=" + accountNumTo + ", dateOfTransfer=" + dateOfTransfer + " ]";
    }
    
}
