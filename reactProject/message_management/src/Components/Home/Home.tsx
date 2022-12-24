import { useSelector } from "react-redux";
import { storeType } from "../../redux/reducers/rootReducer";
import ContainerManager from "../ContainerManager/ContainerManager";
import User from "../User/User";
import './Home.scss'

export default function Home() {

    const currentUser = useSelector((store:storeType)=>store.UserReducer);
    return(
       <div className="Home ">
           
           {currentUser.id==7? <ContainerManager></ContainerManager>:<User></User>}
       </div>
       )
}
