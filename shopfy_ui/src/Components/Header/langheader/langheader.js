import style from './langheader.module.css';

function LangHeader() {
    return (
        <div>
            <div>
                            You are a student and students get 20% discount.
            </div>
            <ul className={`${style.ul}`}>
                <li> Store Locator </li>
                <li> Order Tracking </li>
                <li> FAQs </li>
                <li> | </li>
                <li> English </li>
                <li> United States (USD $) </li>
            </ul>
            <hr className='m-0'></hr>
           
        </div>
    );
}

export default LangHeader;