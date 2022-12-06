/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Entities;

import java.io.Serializable;
import java.util.Collection;
import javax.persistence.Basic;
import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Table;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlTransient;

/**
 *
 * @author torge
 */
@Entity
@Table(name = "USERS")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "Users.findAll", query = "SELECT u FROM Users u"),
    @NamedQuery(name = "Users.findByAccountNumber", query = "SELECT u FROM Users u WHERE u.accountNumber = :accountNumber"),
    @NamedQuery(name = "Users.findByTz", query = "SELECT u FROM Users u WHERE u.tz = :tz"),
    @NamedQuery(name = "Users.findByAccountOwnerName", query = "SELECT u FROM Users u WHERE u.accountOwnerName = :accountOwnerName"),
    @NamedQuery(name = "Users.findByEmail", query = "SELECT u FROM Users u WHERE u.email = :email"),
    @NamedQuery(name = "Users.findByTelephone", query = "SELECT u FROM Users u WHERE u.telephone = :telephone"),
    @NamedQuery(name = "Users.findByCellphone", query = "SELECT u FROM Users u WHERE u.cellphone = :cellphone"),
    @NamedQuery(name = "Users.findByPassword", query = "SELECT u FROM Users u WHERE u.password = :password"),
    @NamedQuery(name = "Users.findByBalance", query = "SELECT u FROM Users u WHERE u.balance = :balance"),
    @NamedQuery(name = "Users.findByEmailAndPassword", query = "SELECT u FROM Users u WHERE u.email = :email AND u.password=:password"),
    @NamedQuery(name = "Users.findByaccountNumberAndPassword", query = "SELECT u FROM Users u WHERE u.accountNumber = :accountNumber AND u.password=:password")})

public class Users implements Serializable {
    private static final long serialVersionUID = 1L;
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Basic(optional = false)
    @Column(name = "ACCOUNT_NUMBER")
    private Integer accountNumber;
    @Column(name = "TZ")
    private String tz;
    @Column(name = "ACCOUNT_OWNER_NAME")
    private String accountOwnerName;
    @Column(name = "EMAIL")
    private String email;
    @Column(name = "TELEPHONE")
    private String telephone;
    @Column(name = "CELLPHONE")
    private String cellphone;
    @Column(name = "PASSWORD")
    private String password;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Column(name = "BALANCE")
    private Double balance;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "users")
    private Collection<StandingOrders> standingOrdersCollection;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "users1")
    private Collection<StandingOrders> standingOrdersCollection1;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "users")
    private Collection<BankTransfers> bankTransfersCollection;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "users1")
    private Collection<BankTransfers> bankTransfersCollection1;

    public Users() {
    }

    public Users(Integer accountNumber) {
        this.accountNumber = accountNumber;
    }

    public Integer getAccountNumber() {
        return accountNumber;
    }

    public void setAccountNumber(Integer accountNumber) {
        this.accountNumber = accountNumber;
    }

    public String getTz() {
        return tz;
    }

    public void setTz(String tz) {
        this.tz = tz;
    }

    public String getAccountOwnerName() {
        return accountOwnerName;
    }

    public void setAccountOwnerName(String accountOwnerName) {
        this.accountOwnerName = accountOwnerName;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getTelephone() {
        return telephone;
    }

    public void setTelephone(String telephone) {
        this.telephone = telephone;
    }

    public String getCellphone() {
        return cellphone;
    }

    public void setCellphone(String cellphone) {
        this.cellphone = cellphone;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public Double getBalance() {
        return balance;
    }

    public void setBalance(Double balance) {
        this.balance = balance;
    }

    @XmlTransient
    public Collection<StandingOrders> getStandingOrdersCollection() {
        return standingOrdersCollection;
    }

    public void setStandingOrdersCollection(Collection<StandingOrders> standingOrdersCollection) {
        this.standingOrdersCollection = standingOrdersCollection;
    }

    @XmlTransient
    public Collection<StandingOrders> getStandingOrdersCollection1() {
        return standingOrdersCollection1;
    }

    public void setStandingOrdersCollection1(Collection<StandingOrders> standingOrdersCollection1) {
        this.standingOrdersCollection1 = standingOrdersCollection1;
    }

    @XmlTransient
    public Collection<BankTransfers> getBankTransfersCollection() {
        return bankTransfersCollection;
    }

    public void setBankTransfersCollection(Collection<BankTransfers> bankTransfersCollection) {
        this.bankTransfersCollection = bankTransfersCollection;
    }

    @XmlTransient
    public Collection<BankTransfers> getBankTransfersCollection1() {
        return bankTransfersCollection1;
    }

    public void setBankTransfersCollection1(Collection<BankTransfers> bankTransfersCollection1) {
        this.bankTransfersCollection1 = bankTransfersCollection1;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (accountNumber != null ? accountNumber.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Users)) {
            return false;
        }
        Users other = (Users) object;
        if ((this.accountNumber == null && other.accountNumber != null) || (this.accountNumber != null && !this.accountNumber.equals(other.accountNumber))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "Entities.Users[ accountNumber=" + accountNumber + " ]";
    }
    
}
