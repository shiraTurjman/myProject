import { useState } from "react";
import { useSelector } from "react-redux";
import { storeType } from "../../redux/reducers/rootReducer";
import { Message } from "../../types/message"

// import './AddMessage.scss'


export default function AddMessage(props:any) {

    
    const currentUser = useSelector((store:storeType)=>store.UserReducer);

    const [isfullForm,setIsFullForm]=useState(0);
    const [myTitle,setTitle]=useState("");
    const [myBody,setBody]=useState("");

    //שומר את הנתון של האינפוט האם מלא או ריק ומעדכן בשינוי
    const setCurTitle=(e:any)=>{
        setTitle(e.target.value);
    }

    const setCurBody=(e:any)=>{
        setBody(e.target.value);
    }
//בודק האם הטופס מלא
    const checkValid = ()=>{
        if(myTitle!="" && myBody!="")
        setIsFullForm(1);
        else
        setIsFullForm(0);
    }
//שולח הודעה חדשה דרך הרף לאבא ששולח לקומפוננטה השניה
    const addMessage = (event: React.FormEvent<HTMLInputElement>) => {
        event.preventDefault();
        const newMessage = {
        title: myTitle,
        body: myBody,
        userId: currentUser.id
      } as unknown as Message;
      props.listApiRef.current.addApi(newMessage);
      //איפוס המשתנים
      setTitle("");
      setBody("");
      setIsFullForm(0);
    }
    return (
        <div className="AddMessage">
            <form onSubmit={(event: any) => { addMessage(event) }}>
                <div className="form-group">
                    <label htmlFor="title">כותרת</label>
                    <input onChange={(e:any)=>{checkValid(); setCurTitle(e)}} type="text" className="form-control" id="title" name="title" defaultValue="" aria-describedby="title" placeholder="Title" value={myTitle} ></input>
                </div>
                <div className="form-group">
                    <label htmlFor="body">גוף הודעה</label>
                    <input onChange={(e:any)=>{checkValid(); setCurBody(e)}} type="text" className="form-control" id="body" placeholder="Body" defaultValue="" value={myBody}></input>
                </div>
                <br></br>
                <button disabled={isfullForm==0} type="submit" className="btn btn-primary">הוסף</button>
            </form>
        </div>
        )
        //בזמן לחיצה ושינוי מעדכן את המשתנים של הוליו ובודק האם תקין
        //הכפתור מאופשר רק אם הטופס מלא

}
