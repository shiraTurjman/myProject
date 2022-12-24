import { useEffect } from "react";
import { Container, Nav, Navbar } from "react-bootstrap";
import { useSelector } from "react-redux";
import { Link, Outlet, useNavigate } from "react-router-dom";
import { storeType } from "../../redux/reducers/rootReducer";
import './Header.scss'




export default function Header() {

    const User = useSelector((store:storeType)=>store.UserReducer);

    const _navigate = useNavigate();
    
    useEffect(() => {
      _navigate("home");
    }, []);

return (
    
    <div className="Header">
        <h1 className="hello">hello {User.name}</h1>
        <Navbar bg="dark" variant="dark" className="nev">
            <Container  >
                <Nav className="me-auto"> 
                <Nav.Link ><Link to="home">Home</Link></Nav.Link>
                <Nav.Link ><Link to="about">About</Link></Nav.Link>
                {User.id==7? <Nav.Link><Link to="home">Add User</Link></Nav.Link>:""}
                </Nav>
            </Container>
        </Navbar>
        <Outlet></Outlet>
    </div>
    
    
)

}