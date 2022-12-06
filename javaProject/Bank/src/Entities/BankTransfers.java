/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Entities;

import java.io.Serializable;
import java.util.Date;
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
@Table(name = "BANK_TRANSFERS")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "BankTransfers.findAll", query = "SELECT b FROM BankTransfers b"),
    @NamedQuery(name="BankTransfers.find3day",query="SELECT b FROM  BankTransfers b WHERE b.bankTransfersPK.dateOfTransfer>=:before"),
    @NamedQuery(name = "BankTransfers.findByAccountNumFrom", query = "SELECT b FROM BankTransfers b WHERE b.bankTransfersPK.accountNumFrom = :accountNumFrom"),
    @NamedQuery(name = "BankTransfers.findByAccountNumTo", query = "SELECT b FROM BankTransfers b WHERE b.bankTransfersPK.accountNumTo = :accountNumTo"),
    @NamedQuery(name = "BankTransfers.findByDateOfTransfer", query = "SELECT b FROM BankTransfers b WHERE b.bankTransfersPK.dateOfTransfer = :dateOfTransfer"),
    @NamedQuery(name = "BankTransfers.findBySumOfTransfer", query = "SELECT b FROM BankTransfers b WHERE b.sumOfTransfer = :sumOfTransfer")})

public class BankTransfers implements Serializable {
    private static final long serialVersionUID = 1L;
    @EmbeddedId
    protected BankTransfersPK bankTransfersPK;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Column(name = "SUM_OF_TRANSFER")
    private Double sumOfTransfer;
    @JoinColumn(name = "ACCOUNT_NUM_TO", referencedColumnName = "ACCOUNT_NUMBER", insertable = false, updatable = false)
    @ManyToOne(optional = false)
    private Users users;
    @JoinColumn(name = "ACCOUNT_NUM_FROM", referencedColumnName = "ACCOUNT_NUMBER", insertable = false, updatable = false)
    @ManyToOne(optional = false)
    private Users users1;

    public BankTransfers() {
    }

    public BankTransfers(BankTransfersPK bankTransfersPK) {
        this.bankTransfersPK = bankTransfersPK;
    }

    public BankTransfers(int accountNumFrom, int accountNumTo, Date dateOfTransfer) {
        this.bankTransfersPK = new BankTransfersPK(accountNumFrom, accountNumTo, dateOfTransfer);
    }

    public BankTransfersPK getBankTransfersPK() {
        return bankTransfersPK;
    }

    public void setBankTransfersPK(BankTransfersPK bankTransfersPK) {
        this.bankTransfersPK = bankTransfersPK;
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
        hash += (bankTransfersPK != null ? bankTransfersPK.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof BankTransfers)) {
            return false;
        }
        BankTransfers other = (BankTransfers) object;
        if ((this.bankTransfersPK == null && other.bankTransfersPK != null) || (this.bankTransfersPK != null && !this.bankTransfersPK.equals(other.bankTransfersPK))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "Entities.BankTransfers[ bankTransfersPK=" + bankTransfersPK + " ]";
    }
    
}
