import Manager from "../Manager/Manager";
import MessageCom from "../MessageCom/MessageCom";

export default function ContainerAdmin(){
    

    return(
        <div className="containerManager">
            <div className="row">
                <div className="col" ><Manager></Manager></div>
                <div className="col"><MessageCom></MessageCom></div>
            </div>
        </div>
    )
}